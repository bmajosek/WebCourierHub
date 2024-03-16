using WebCourierHub.Classes;
using WebCourierHub.Models;

namespace WebCourierHub.Classes
{
    public static class Mapper
    {
        public static Inquiry ToModel(InquiryDto inquireDTO)
        {
            return new Inquiry
            {
                Package = MapPackageDtoToModel(inquireDTO.Package),
                PickupDate = inquireDTO.PickupDate,
                Source = MapAddressDtoToModel(inquireDTO.Source),
                DeliveryDate = inquireDTO.DeliveryDate,
                Destination = MapAddressDtoToModel(inquireDTO.Destination),
                Priority = inquireDTO.Priority,
                DeliveryAtWeekend = inquireDTO.DeliveryAtWeekend,
                Company = inquireDTO.Company != null ? MapCompanyDtoToModel(inquireDTO.Company) : null,
                Status = inquireDTO.Status != null ? MapStatusDtoToModel(inquireDTO.Status) : null,
                ClientId = inquireDTO.ClientId
            };
        }

        public static Address MapAddressDtoToModel(AddressDto addressDto)
        {
            return new Address
            {
                Street = addressDto.Street,
                City = addressDto.City,
                State = addressDto.State,
                PostalCode = addressDto.PostalCode,
                Country = addressDto.Country
            };
        }

        public static Company MapCompanyDtoToModel(CompanyDto companyDto)
        {
            return new Company
            {
                Name = companyDto.Name,
                Address = MapAddressDtoToModel(companyDto.Address),
                NIP = companyDto.NIP
            };
        }

        public static Status MapStatusDtoToModel(StatusDto statusDto)
        {
            return new Status
            {
                Name = statusDto.Name
            };
        }

        public static Package MapPackageDtoToModel(PackageDto packageDto)
        {
            return new Package
            {
                Length = packageDto.Length,
                Width = packageDto.Width,
                Height = packageDto.Height,
                Weight = packageDto.Weight
            };
        }

        public static InquiryDto ToDto(Inquiry inquiry)
        {
            return new InquiryDto
            {
                Package = MapPackageToDto(inquiry.Package),
                PickupDate = inquiry.PickupDate,
                Source = MapAddressToDto(inquiry.Source),
                DeliveryDate = inquiry.DeliveryDate,
                Destination = MapAddressToDto(inquiry.Destination),
                Priority = inquiry.Priority ?? false,
                DeliveryAtWeekend = inquiry.DeliveryAtWeekend ?? false,
                Company = inquiry.Company != null ? MapCompanyToDto(inquiry.Company) : null,
                Status = inquiry.Status != null ? MapStatusToDto(inquiry.Status) : null,
                Id = inquiry.Id,
                ClientId = inquiry.ClientId
            };
        }

        public static AddressDto MapAddressToDto(Address address)
        {
            return new AddressDto
            {
                Street = address.Street,
                City = address.City,
                State = address.State,
                PostalCode = address.PostalCode,
                Country = address.Country
            };
        }

        public static CompanyDto MapCompanyToDto(Company company)
        {
            return new CompanyDto
            {
                Name = company.Name,
                Address = MapAddressToDto(company.Address),
                NIP = company.NIP
            };
        }

        public static StatusDto MapStatusToDto(Status status)
        {
            return new StatusDto
            {
                Name = status.Name
            };
        }

        public static PackageDto MapPackageToDto(Package package)
        {
            return new PackageDto
            {
                Length = package.Length,
                Width = package.Width,
                Height = package.Height,
                Weight = package.Weight
            };
        }

        public static OfferDto MapOfferToDto(Offer offer)
        {
            return new OfferDto
            {
                Id = offer.Id,
                StatusName = offer.Status?.Name,
                CompanyName = offer.Company?.Name,
                CreatedTime = offer.CreatedTime,
                TotalPrice = offer.TotalPrice,
                ClientName = offer.ClientId
            };
        }

        public static Delivery MapDeliveryDtoToModel(DeliveryDto deliveryDto)
        {
            return new Delivery
            {
                Id = deliveryDto.Id,
                Status = new Status { Name = deliveryDto.StatusName },
                Company = new Company { Name = deliveryDto.CompanyName },
                DateOfDelivery = deliveryDto.DateOfDelivery,
                CourierName = deliveryDto.CourierName
            };
        }

        public static DeliveryDto MapDeliveryToDto(Delivery delivery)
        {
            return new DeliveryDto
            {
                Id = delivery.Id,
                StatusName = delivery.Status?.Name,
                CompanyName = delivery.Company?.Name,
                DateOfDelivery = delivery.DateOfDelivery,
                CourierName = delivery.CourierName
            };
        }

        public static Offer MapOfferDtoToModel(OfferDto offerDto)
        {
            return new Offer
            {
                Id = offerDto.Id,
                Status = new Status { Name = offerDto.StatusName },
                Company = new Company { Name = offerDto.CompanyName },
                CreatedTime = offerDto.CreatedTime,
                TotalPrice = offerDto.TotalPrice,
                ClientId = offerDto.ClientName
            };
        }
    }
}