using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using WebCourierApi.Model.DTO;
using WebCourierApi.Model.ValueObjects;
using WebCourierHub.Classes;
using WebCourierHub.Data;
using WebCourierHub.Models;
using WebCourierHub.Support.ApiConfig;

namespace WebCourierHub.Controllers.Api
{
    [Route("api/Inquiries")]
    [ApiController]
    public class InquiryApiController : ControllerBase
    {
        private readonly WebCourierHubDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IApiConfig _internalApiConfig;
        private readonly JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler(); // Do podłączenia do MP

        public InquiryApiController(WebCourierHubDbContext context, IHttpClientFactory httpClientFactory, [FromKeyedServices("Internal")] IApiConfig internalApiConfig)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _internalApiConfig = internalApiConfig;
        }

        [HttpGet("ListAll")]
        public IActionResult ListAll()
        {
            var listOfInquiries = _context.Inquiry.ToList();

            return new JsonResult(listOfInquiries);
        }

        [HttpGet("GetById/{inquiryId}")]
        public IActionResult GetById(int inquiryId)
        {
            var inquiry = _context.Inquiry.FirstOrDefault(x => x.Id == inquiryId);

            return new JsonResult(inquiry);
        }

        [HttpGet("GetTheLatest")]
        public IActionResult GetTheLatest()
        {
            var todayDate = DateTime.Now;
            var inquiry = _context.Inquiry.Where(x => x.PickupDate != null && (todayDate - x.PickupDate.Value).TotalDays <= 30).OrderBy(x => x.PickupDate).ToList();

            return new JsonResult(inquiry);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(InquiryDto inquiryDto)
        {
            var httpClient = _httpClientFactory.CreateClient();
            _internalApiConfig.AddCredentialsTo(httpClient);
            var inquireDTO = ConvertToInternalApiInquireCreationDto(inquiryDto);
            var inquireDTO2 = ConvertToInternalApiInquire_2_CreationDto(inquiryDto);
            var inquireDTO3 = ConvertToInternalApiNewInquireCreationDto(inquiryDto);
            var json = JsonConvert.SerializeObject(inquireDTO);
            var json2 = JsonConvert.SerializeObject(inquireDTO2);
            var json3 = JsonConvert.SerializeObject(inquireDTO3);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var content2 = new StringContent(json2, Encoding.UTF8, "application/json");
            var content3 = new StringContent(json3, Encoding.UTF8, "application/json");
            var accessToken = await GetAccessTokenAsync();

            try
            {
                // Wysyłanie zapytania POST do utworzenia nowego inquiry
                var response = await httpClient.PostAsync($"{_internalApiConfig.Url}/api/Inquiries", content);
                if (response.IsSuccessStatusCode)
                {
                    var inquiryResponse = await response.Content.ReadAsStringAsync();
                    var createdInquiry = JsonConvert.DeserializeObject<WebCourierApi.Model.DTO.InquireDetailsDTO>(inquiryResponse);

                    // Request offers for the created inquiry

                    // ----------- Połączenie do MP -------------------
                    var jwt = GenerateTokenForProviderApi();
                    var httpClientMP = new HttpClient();
                    httpClientMP.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                    // ------------- Wysyłanie zaproszenia również do API prowadzacego ----------------
                    var teacherHttpClient = _httpClientFactory.CreateClient();

                    // Autoryzacja
                    var clientId = "team2a";
                    var clientSecret = "167F6D7C-806D-4E20-B57E-E94EBFE45519";

                    teacherHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    var postOffersTask = httpClient.PostAsync($"{_internalApiConfig.Url}/api/Inquiries/{createdInquiry.Id}/offers", null);
                    var postMPInquiryTask = httpClientMP.PostAsync("https://courierapi.mangoplant-8d8755bd.germanywestcentral.azurecontainerapps.io/inquire", content3);
                    var postTeacherInquiryTask = teacherHttpClient.PostAsync("https://mini.currier.api.snet.com.pl/Inquires", content2);

                    // Czekaj na zakończenie wszystkich zadań
                    await Task.WhenAll(postOffersTask, postMPInquiryTask, postTeacherInquiryTask);

                    var requestOffersResponse = await postOffersTask;
                    var MPResponse = await postMPInquiryTask;
                    var teacherApiInquiryResponse = await postTeacherInquiryTask;

                    var offersContent = await requestOffersResponse.Content.ReadAsStringAsync();
                    var offers = JsonConvert.DeserializeObject<IEnumerable<OfferAdapterDTO>>(offersContent);
                    var offersContent3 = await MPResponse.Content.ReadAsStringAsync();
                    var offers3 = JsonConvert.DeserializeObject<NewOfferAdapterDTO>(offersContent3);
                    var offersContent2 = await teacherApiInquiryResponse.Content.ReadAsStringAsync();
                    var offers2 = JsonConvert.DeserializeObject<OfferAdapterDTO2>(offersContent2);
                    var inquiry = Mapper.ToModel(inquiryDto);
                    _context.Add(inquiry);
                    await _context.SaveChangesAsync();
                    return Ok(new { Inquiry = createdInquiry, Offers = offers, offers2 = offers2, offers3 = offers3 });
                }

                return StatusCode((int)response.StatusCode, response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                // Obsługa błędów
                return StatusCode(500, ex.Message);
            }
        }

        public string GenerateTokenForProviderApi() // Guid providerUuid
        {
            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("108e6dcb46d73e10c955f498763610f07f21ed334ec6b56864e8f611956388d0")); //  + providerUuid.ToString().ToUpper())

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "couriernet"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var credentials = new SigningCredentials(securityKey, algorithm: SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
            issuer: "couriernet.courierApi",
            expires: DateTime.UtcNow.AddMinutes(60),
            signingCredentials: credentials,
            claims: claims
            );

            return _tokenHandler.WriteToken(token);
        }

        public async Task<string> GetAccessTokenAsync()
        {
            var clientId = "team2a";
            var clientSecret = "167F6D7C-806D-4E20-B57E-E94EBFE45519";
            var tokenUrl = "https://indentitymanager.snet.com.pl/connect/token";
            var scope = "MiNI.Courier.API"; // lub inny zakres dostępu

            var httpClient = new HttpClient();

            var tokenRequest = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", clientSecret),
                new KeyValuePair<string, string>("scope", scope)
            };

            var tokenResponse = await httpClient.PostAsync(tokenUrl, new FormUrlEncodedContent(tokenRequest));
            tokenResponse.EnsureSuccessStatusCode();

            var tokenContent = await tokenResponse.Content.ReadAsStringAsync();
            var tokenJson = JObject.Parse(tokenContent);

            var accessToken = tokenJson.GetValue("access_token").ToString();
            return accessToken;
        }

        [HttpPost("offers/pick/{company}")]
        public async Task<ActionResult> PickOffer(int company, [FromBody] OfferSelection selection)
        {
            string offerNumber = selection.OfferNumber;
            string id = selection.Id;
            var httpClient = _httpClientFactory.CreateClient();
            _internalApiConfig.AddCredentialsTo(httpClient);

            // Pobierz adres e-mail z claimów
            var emailClaim = User.Claims.FirstOrDefault(c => c.Type == "nickname");
            var emailAddress = emailClaim?.Value;
            var companyName = company == 0 ? "Nasza firma :) " : company == 1 ? "Prowadzacego :) " : "Twoj Karton";
            if (emailAddress != null && !emailAddress.Contains("@"))
            {
                emailAddress += "@gmail.com";
            }
            var client = new Client
            {
                FirstName = User.Identity.Name,
                EmailAddress = emailAddress ?? "test@gmail.com",
                CompanyName = companyName
            };
            if (company == 0)
            {
                var json = JsonConvert.SerializeObject(client);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    var response = await httpClient.PostAsync($"{_internalApiConfig.Url}/api/Inquiries/{id}/offers/pick/{offerNumber}", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var newOffer = new Offer()
                        {
                            CreatedTime = DateTime.Now,
                            ClientId = User.Identity?.Name,
                            TotalPrice = selection.TotalPrice
                        };
                        await _context.AddAsync(newOffer);
                        return Ok(await response.Content.ReadAsStringAsync());
                    }
                    else
                    {
                        return StatusCode((int)response.StatusCode, response.Content.ReadAsStringAsync().Result);
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }
            else if (company == 1)
            {
                var teacherHttpClient = _httpClientFactory.CreateClient();
                var clientId = "team2a";
                var clientSecret = "167F6D7C-806D-4E20-B57E-E94EBFE45519";
                var accessToken = await GetAccessTokenAsync();

                teacherHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var newInquiryDto = CreateNewInquiryDTO(id.ToString());
                var newInquiryJson = JsonConvert.SerializeObject(newInquiryDto);
                var newInquiryContent = new StringContent(newInquiryJson, Encoding.UTF8, "application/json");
                var newInquiryResponse = await teacherHttpClient.PostAsync("https://mini.currier.api.snet.com.pl/Offers", newInquiryContent); // tutaj dostajemy ID od prowadzącego
                var newInquiryResult = await newInquiryResponse.Content.ReadAsStringAsync();
                var respWithID = JsonConvert.DeserializeObject<OfferRequestDTO>(newInquiryResult); // tutaj są offerRequestId oraz validTo
                return Ok(await newInquiryResponse.Content.ReadAsStringAsync());
            }
            else if (company == 2)
            {
                var userInfo = CreateNewUserInformation(offerNumber);
                userInfo.EmailAddress = client.EmailAddress;
                var json = JsonConvert.SerializeObject(userInfo);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var jwt = GenerateTokenForProviderApi();
                var httpClientMP = new HttpClient();
                httpClientMP.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                var MPResponse = await httpClientMP.PostAsync("https://courierapi.mangoplant-8d8755bd.germanywestcentral.azurecontainerapps.io/order", content);
                var offerResponse = await MPResponse.Content.ReadAsStringAsync();

                return Ok(await MPResponse.Content.ReadAsStringAsync());
            }
            return BadRequest("zly numer company");
        }

        private InquireCreationDTO ConvertToInternalApiInquireCreationDto(InquiryDto inquiryDto)
        {
            return new InquireCreationDTO
            {
                Package = new WebCourierApi.Model.ValueObjects.Package
                {
                    LengthCM = inquiryDto.Package.Length,
                    WidthCM = inquiryDto.Package.Width,
                    HeightCM = inquiryDto.Package.Height,
                    WeightKG = inquiryDto.Package.Weight
                },
                PickupDate = DateOnly.FromDateTime(inquiryDto.PickupDate.Value),
                PickupAddress = new AddressDTO
                {
                    Country = inquiryDto.Source.Country,
                    ZipCode = inquiryDto.Source.PostalCode,
                    Town = inquiryDto.Source.City,
                    Street = inquiryDto.Source.Street,
                    BuildingNumber = 0
                },
                DeliveryDate = DateOnly.FromDateTime(inquiryDto.DeliveryDate.Value),
                DeliveryAddress = new AddressDTO
                {
                    Country = inquiryDto.Destination.Country,
                    ZipCode = inquiryDto.Destination.PostalCode,
                    Town = inquiryDto.Destination.City,
                    Street = inquiryDto.Destination.Street,
                    BuildingNumber = 0
                },
                DeliveryOptions = new DeliveryOptions
                {
                    IsForCompany = true,
                    HighPriority = inquiryDto.Priority,
                    WeekendDelivery = inquiryDto.DeliveryAtWeekend
                }
            };
        }

        private InquireCreation2DTO ConvertToInternalApiInquire_2_CreationDto(InquiryDto inquiryDto)
        {
            return new InquireCreation2DTO
            {
                Dimensions = new DimensionsDTO
                {
                    Length = inquiryDto.Package.Length,
                    Width = inquiryDto.Package.Width,
                    Height = inquiryDto.Package.Height,
                    DimensionUnit = "Meters"
                },
                Currency = "Pln",
                Weight = inquiryDto.Package.Weight,
                WeightUnit = "Kilograms",
                Source = new AddressDTO2
                {
                    HouseNumber = "24",
                    ApartmentNumber = "24",
                    Street = inquiryDto.Source.Street,
                    City = inquiryDto.Source.City,
                    ZipCode = inquiryDto.Source.PostalCode,
                    Country = inquiryDto.Source.Country
                },
                Destination = new AddressDTO2
                {
                    HouseNumber = "24",
                    ApartmentNumber = "23",
                    Street = inquiryDto.Destination.Street,
                    City = inquiryDto.Destination.City,
                    ZipCode = inquiryDto.Destination.PostalCode,
                    Country = inquiryDto.Destination.Country
                },
                PickupDate = inquiryDto.PickupDate.Value,
                DeliveryDay = inquiryDto.DeliveryDate.Value,
                DeliveryInWeekend = inquiryDto.DeliveryAtWeekend,
                Priority = "Low",
                VipPackage = inquiryDto.Priority,
                IsCompany = true
            };
        }

        // Powinno być w innym miejscu, ale nie ma czasu:
        public class NewInquireCreationDTO
        {
            public PackageDTO3 Package { get; set; }
            public AddressDTO3 Source { get; set; }
            public AddressDTO3 Destination { get; set; }
            public DateTime PickupDate { get; set; }
            public DateTime DeliveryDate { get; set; }
            public bool WeekendDelivery { get; set; }
        }

        public class PackageDTO3
        {
            public double Length { get; set; }
            public double Width { get; set; }
            public double Height { get; set; }
            public double Weight { get; set; }
        }

        public class AddressDTO3
        {
            public string BuildingNumber { get; set; }
            public string ApartmentNumber { get; set; }
            public string Street { get; set; }
            public string City { get; set; }
            public string ZipCode { get; set; }
            public string Country { get; set; }
        }

        private NewInquireCreationDTO ConvertToInternalApiNewInquireCreationDto(InquiryDto inquiryDto)
        {
            return new NewInquireCreationDTO
            {
                Package = new PackageDTO3
                {
                    Length = inquiryDto.Package.Length,
                    Width = inquiryDto.Package.Width,
                    Height = inquiryDto.Package.Height,
                    Weight = inquiryDto.Package.Weight
                },
                Source = new AddressDTO3
                {
                    BuildingNumber = "string",
                    ApartmentNumber = "string",
                    Street = "string",
                    City = "string",
                    ZipCode = "string",
                    Country = "string"
                },
                Destination = new AddressDTO3
                {
                    BuildingNumber = "string",
                    ApartmentNumber = "string",
                    Street = "string",
                    City = "string",
                    ZipCode = "string",
                    Country = "string"
                },
                PickupDate = inquiryDto.PickupDate.Value,
                DeliveryDate = inquiryDto.DeliveryDate.Value,
                WeekendDelivery = inquiryDto.DeliveryAtWeekend
            };
        }

        private NewInquiryDTO CreateNewInquiryDTO(string inquiryId)
        {
            // Wprowadź właściwe dane
            string name = "string";
            string email = "string";

            var address = new AddressDTO4
            {
                HouseNumber = "string",
                ApartmentNumber = "string",
                Street = "string",
                City = "string",
                ZipCode = "string",
                Country = "string" // do poprawienia
            };

            return new NewInquiryDTO
            {
                InquiryId = inquiryId,
                Name = name,
                Email = email,
                Address = address
            };
        }

        private UserInformation CreateNewUserInformation(string offerId)
        {
            return new UserInformation
            {
                OfferUuid = offerId,
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "john.doe@example.com",
                PhoneNumber = "123-456-7890",
                Address = new UserInformation.AddressDetails
                {
                    BuildingNumber = "100",
                    ApartmentNumber = "101",
                    Street = "Main Street",
                    City = "Anytown",
                    Zipcode = "12345",
                    Country = "Fantasyland"
                }
            };
        }
    }
}