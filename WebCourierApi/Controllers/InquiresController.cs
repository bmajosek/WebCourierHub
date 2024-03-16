using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebCourierApi.BusinessLogic;
using WebCourierApi.Data;
using WebCourierApi.Data.Repositories;
using WebCourierApi.Model.DTO;
using WebCourierApi.Model;
using WebCourierApi.Model.Entities;
using WebCourierApi.Model.ValueObjects;
using WebCourierApi.Utils.ApiKey.Filters;
using WebCourierApi.Utils.DynamicQuery.Queries;

namespace WebCourierApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InquiriesController : ControllerBase
    {
        private readonly IInquireRepository _repository;
        private readonly IEnumerable<IOfferStrategy> _offerStrategies;
        private readonly IMapper _mapper;
        private readonly INationalRepository _nationalRepository;

        public InquiriesController(IInquireRepository repository, IEnumerable<IOfferStrategy> offerStrategies, IMapper mapper, INationalRepository nationalRepository)
        {
            _repository = repository;
            _offerStrategies = offerStrategies;
            _mapper = mapper;
            _nationalRepository = nationalRepository;
        }

        // GET: api/Inquiries
        [ExternalApiKey]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InquireDetailsDTO>>> GetInquires([FromQuery] InquireDynamicQuery query)
        {
            var inquires = await _repository.FindAll(query);

            return Ok(inquires.Select(inquire => _mapper.Map<InquireDetailsDTO>(inquire)).ToArray());
        }

        // GET: api/Inquiries/5
        [ExternalApiKey]
        [HttpGet("{id}")]
        public async Task<ActionResult<InquireDetailsDTO>> GetInquire(int id)
        {
            var inquire = await _repository.FindById(id);

            return Ok(_mapper.Map<InquireDetailsDTO>(inquire));
        }

        // POST: api/Inquiries
        [ExternalApiKey]
        [HttpPost]
        public async Task<ActionResult<InquireDetailsDTO>> PostInquire(InquireCreationDTO inquireDTO)
        {
            var pickupCountryId = await _nationalRepository.CountryIdByName(inquireDTO.PickupAddress.Country);
            var deliveryCountryId = await _nationalRepository.CountryIdByName(inquireDTO.DeliveryAddress.Country);

            var inquire = await _repository.Add(_mapper.Map<Inquire>(inquireDTO, opt =>
            {
                opt.Items[ApiDTOMapper.PickupCountryIdKey] = pickupCountryId;
                opt.Items[ApiDTOMapper.DeliveryCountryIdKey] = deliveryCountryId;
            }));

            return CreatedAtAction("GetInquire", new { id = inquire.Id }, _mapper.Map<InquireDetailsDTO>(inquire));
        }

        // DELETE: api/Inquiries/5
        [InternalApiKey]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInquire(int id)
        {
            await _repository.Delete(id);

            return NoContent();
        }

        // GET: api/Inquiries/5/offers
        [ExternalApiKey]
        [HttpGet("{id}/offers")]
        public async Task<ActionResult<IEnumerable<OfferDTO>>> GetOffers(int id)
        {
            var offers = await _repository.GetOffers(id);

            return Ok(offers.Select(offer => _mapper.Map<OfferDTO>(offer)));
        }

        // POST: api/Inquiries/5/offers
        [ExternalApiKey]
        [HttpPost("{id}/offers")]
        public async Task<ActionResult<IEnumerable<OfferDTO>>> RequestOffers(int id)
        {
            var inquire = await _repository.FindById(id);
            var offerNumber = 0;
            var offers = _offerStrategies
                .Where(strategy => strategy.IsApplicable(inquire))
                .Select(strategy => new Offer()
                {
                    OfferNumber = offerNumber++,
                    ValidTo = strategy.EvalValidTo(inquire),
                    Pricing = strategy.EvalPricing(inquire)
                })
                .ToList();
            var offersAdded = await _repository.AddOffers(inquire, offers);

            return CreatedAtAction("GetOffers", new { id = inquire.Id }, _mapper.Map<List<OfferDTO>>(offersAdded));
        }

        // POST: api/Inquiries/5/offers/pick/2
        [ExternalApiKey]
        [HttpPost("{id}/offers/pick/{offerNumber}")]
        public async Task<ActionResult<DeliveryDTO>> PickOffer(int id, int offerNumber, Client client)
        {
            var delivery = await _repository.PickOffer(id, offerNumber, client);

            return CreatedAtAction("GetDelivery", "Deliveries", new { id = delivery.Id }, _mapper.Map<DeliveryDTO>(delivery));
        }
    }
}