using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebCourierApi.Data;
using WebCourierApi.Data.Repositories;
using WebCourierApi.Model.DTO;
using WebCourierApi.Model.Enums;
using WebCourierApi.Model.Entities;
using WebCourierApi.Utils.ApiKey.Filters;
using WebCourierApi.Utils.DynamicQuery.Queries;
using WebCourierApi.Utils.Exceptions;
using WebCourierApi.Utils.MailService;

namespace WebCourierApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveriesController : ControllerBase
    {
        private readonly IDeliveryRepository _repository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public DeliveriesController(IDeliveryRepository repository, IMapper mapper, IEmailService emailService)
        {
            _repository = repository;
            _mapper = mapper;
            _emailService = emailService;
        }

        // GET: api/Deliveries/5
        [ExternalApiKey]
        [HttpGet("{id}")]
        public async Task<ActionResult<DeliveryDTO>> GetDelivery(int id)
        {
            var delivery = await _repository.FindById(id);

            return Ok(_mapper.Map<DeliveryDTO>(delivery));
        }

        // GET: api/Deliveries
        [ExternalApiKey]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeliveryDTO>>> GetDeliveries([FromQuery] DeliveryDynamicQuery query)
        {
            var deliveries = await _repository.FindAll(query);

            return Ok(deliveries.Select(delivery => _mapper.Map<DeliveryDTO>(delivery)));
        }

        // DELETE: api/Deliveries/5
        [InternalApiKey]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDelivery(int id)
        {
            await _repository.Delete(id);

            return NoContent();
        }

        // PATCH: api/Deliveries/5/accept
        [InternalApiKey]
        [HttpPatch("{id}/accept")]
        public async Task<ActionResult<DeliveryDTO>> AcceptDelivery(int id)
        {
            var delivery = await _repository.FindById(id);

            if (delivery.RequestStatus != RequestStatus.Pending)
                throw new InvalidActionException("Unable to accept delivery request. Only pending requests can be accepted.");

            delivery.RequestStatus = RequestStatus.Accepted;
            delivery.Process = new DeliveryProcess();
            await _repository.Save(delivery);
            await _emailService.SendEmailAsync(delivery.Client.EmailAddress, "WebCourierHub accepted offer", $"Hello, \n your offer has been accepted and will be sent to {delivery.Inquire.DeliveryAddress}");
            return Ok(_mapper.Map<DeliveryDTO>(delivery));
        }

        // PATCH: api/Deliveries/5/reject
        [InternalApiKey]
        [HttpPatch("{id}/reject")]
        public async Task<ActionResult<DeliveryDTO>> RejectDelivery(int id)
        {
            var delivery = await _repository.FindById(id);

            if (delivery.RequestStatus != RequestStatus.Pending)
                throw new InvalidActionException("Unable to reject delivery request. Only pending requests can be rejected.");

            delivery.RequestStatus = RequestStatus.Rejected;
            await _repository.Save(delivery);
            await _emailService.SendEmailAsync(delivery.Client.EmailAddress, "WebCourierHub rejected offer", $"Hello, \n your offer has been rejected");

            return Ok(_mapper.Map<DeliveryDTO>(delivery));
        }

        // PATCH: api/Deliveries/5/pickup
        [InternalApiKey]
        [HttpPatch("{id}/pickup")]
        public async Task<ActionResult<DeliveryDTO>> PickupDelivery(int id, string courierName, DateTime pickupTime)
        {
            var delivery = await _repository.FindById(id);

            if (delivery.RequestStatus != RequestStatus.Accepted ||
                delivery.Process == null ||
                delivery.Process.DeliveryStatus != DeliveryStatus.Started)
                throw new InvalidActionException("Unable to mark delivery as 'Picked up'. Only accepted deliveries can be marked as 'Picked up'.");

            delivery.Process.PickupCourierName = courierName;
            delivery.Process.PickupTimestamp = pickupTime;

            await _repository.Save(delivery);
            return Ok(_mapper.Map<DeliveryDTO>(delivery));
        }

        // PATCH: api/Deliveries/5/fulfil
        [InternalApiKey]
        [HttpPatch("{id}/fulfil")]
        public async Task<ActionResult<DeliveryDTO>> FulfilDelivery(int id, string courierName, DateTime deliveryTime)
        {
            var delivery = await _repository.FindById(id);

            if (delivery.RequestStatus != RequestStatus.Accepted ||
                delivery.Process == null ||
                delivery.Process.DeliveryStatus != DeliveryStatus.PickedUp)
                throw new InvalidActionException("Unable to mark delivery as 'Delivered'. Only picked up deliveries can be marked as 'Delivered'.");

            delivery.Process.DeliveryTimestamp = deliveryTime;
            delivery.Process.IsDelivered = true;
            delivery.Process.DeliveryCourierName = courierName;

            await _repository.Save(delivery);
            return Ok(_mapper.Map<DeliveryDTO>(delivery));
        }

        // PATCH: api/Deliveries/5/giveup
        [InternalApiKey]
        [HttpPatch("{id}/giveup")]
        public async Task<ActionResult<DeliveryDTO>> GiveupDelivery(int id, string courierName, DateTime deliveryTime, string reason)
        {
            var delivery = await _repository.FindById(id);

            if (delivery.RequestStatus != RequestStatus.Accepted ||
                delivery.Process == null ||
                delivery.Process.DeliveryStatus != DeliveryStatus.PickedUp)
                throw new InvalidActionException("Unable to mark delivery as 'Cannot deliver'. Only picked up deliveries can be marked as 'Cannot deliver'.");

            delivery.Process.DeliveryTimestamp = deliveryTime;
            delivery.Process.IsDelivered = false;
            delivery.Process.DeliveryCourierName = courierName;
            delivery.Process.Notes = reason;

            await _repository.Save(delivery);

            return Ok(_mapper.Map<DeliveryDTO>(delivery));
        }
    }
}