using Microsoft.AspNetCore.Mvc;
using WebCourierHub.Data;
using WebCourierHub.Models;

namespace WebCourierHub.Controllers.Api
{
	[Route("api/Offers")]
	[ApiController]
	public class OffersApiController : ControllerBase
	{
		private readonly WebCourierHubDbContext _context;

		public OffersApiController(WebCourierHubDbContext context)
		{
			_context = context;
		}

		[HttpPut("Accept/{id}")]
		public IActionResult Accept(int id)
		{
			var offer = _context.Offers.Where(x => x.Id == id).FirstOrDefault();
			if (offer == null)
			{
				return NotFound();
			}

			offer.Status = _context.Status.Where(x => x.Name == "Accepted").FirstOrDefault();

			var newDelivery = new Delivery()
			{
				Company = offer.Company,
				Status = _context.Status.Where(x => x.Name == "Accepted").FirstOrDefault()
			};

			_context.Delivery.Add(newDelivery);
			_context.SaveChanges();

			return Ok(); // change this
		}

		[HttpPut("Delete/{id}")]
		public IActionResult Delete(int id)
		{
			var offer = _context.Offers.Where(x => x.Id == id).FirstOrDefault();
			if (offer == null)
			{
				return NotFound();
			}

			offer.Status = _context.Status.Where(x => x.Name == "Deleted").FirstOrDefault();

			return Ok(); // change this
		}
	}
}