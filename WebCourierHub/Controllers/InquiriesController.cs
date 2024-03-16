using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebCourierHub.Classes;
using WebCourierHub.Data;

namespace WebCourierHub.Controllers
{
    [Authorize]
    [Route("Inquiries")]
    public class InquiriesController : Controller
    {
        private readonly WebCourierHubDbContext _context;

        public InquiriesController(WebCourierHubDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var company = _context.User.FirstOrDefault(x => User.Identity.Name != null && x.IdentityName == User.Identity.Name)?.Company;
            var inquires = _context.Inquiry.Include(x => x.Source).Include(x => x.Destination).Include(x => x.Status).Include(x => x.Package).Where(x => company != null || x.Company != company).Select(x => Mapper.ToDto(x)).ToList();
            var companyName = _context.User.FirstOrDefault(x => x.IdentityName == User.Identity.Name)?.Company?.Name;
            var inquiriesDto = new InquiriesDto()
            {
                Inquiries = inquires,
                Role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? "Client",
                IdentityName = User.Identity.Name,
                CompanyName = companyName
            };
            return View(inquiriesDto);
        }

        [Route("GetTheLatest/Client/{id}/{type}")]
        public IActionResult GetTheLatest(string id, string type)
        {
            var todayDate = DateTime.Now;
            if (type == "Client")
            {
                var inquires = _context.Inquiry
                .Include(x => x.Source)
                .Include(x => x.Destination)
                .Include(x => x.Status)
                .Include(x => x.Package)
                .Where(x => x.PickupDate != null && x.ClientId == id)
                .AsEnumerable()
                .Where(x => (todayDate - x.PickupDate.Value).TotalDays <= 30)
                .Select(x => Mapper.ToDto(x))
                .ToList();

                var companyName = _context.User.FirstOrDefault(x => x.IdentityName == id)?.Company?.Name;

                var inquiriesDto = new InquiriesDto()
                {
                    Inquiries = inquires,
                    Role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? "Client",
                    IdentityName = id,
                    CompanyName = companyName
                };

                return View("Index", inquiriesDto);
            }
            else
            {
                var inquires = _context.Inquiry
                .Include(x => x.Source)
                .Include(x => x.Destination)
                .Include(x => x.Status)
                .Include(x => x.Package)
                .Where(x => x.PickupDate != null && x.Company.Name == id)
                .AsEnumerable()
                .Where(x => (todayDate - x.PickupDate.Value).TotalDays <= 30)
                .Select(x => Mapper.ToDto(x))
                .ToList();

                var inquiriesDto = new InquiriesDto()
                {
                    Inquiries = inquires,
                    Role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? "Client",
                    IdentityName = User.Identity.Name,
                    CompanyName = id
                };

                return View("Index", inquiriesDto);
            }
        }

        [Route("GetCompany/{companyName}")]
        public IActionResult GetCompany(string companyName)
        {
            var inquires = _context.Inquiry.Include(x => x.Source).Include(x => x.Destination).Include(x => x.Status).Include(x => x.Package).Where(x => x.Company.Name == companyName).OrderBy(x => x.PickupDate).Select(x => Mapper.ToDto(x)).ToList();

            var inquiriesDto = new InquiriesDto()
            {
                Inquiries = inquires,
                Role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? "Client",
                IdentityName = User.Identity.Name,
                CompanyName = companyName
            };
            return View("Index", inquiriesDto);
        }

        [Route("GetMy/{clientId}")]
        public IActionResult GetMy(string clientId)
        {
            var inquires = _context.Inquiry
                .Include(x => x.Source)
                .Include(x => x.Destination)
                .Include(x => x.Status)
                .Include(x => x.Package)
                .Where(x => x.ClientId == clientId)
                .Select(x => Mapper.ToDto(x))
                .ToList();

            var companyName = _context.User.FirstOrDefault(x => x.IdentityName == clientId)?.Company?.Name;

            var inquiriesDto = new InquiriesDto()
            {
                Inquiries = inquires,
                Role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? "Client",
                IdentityName = clientId,
                CompanyName = companyName
            };
            return View("Index", inquiriesDto);
        }

        [Route("Details/{id}")]
        public IActionResult Details(int id)
        {
            var inquiry = _context.Inquiry.Include(x => x.Company).Include(x => x.Source).Include(x => x.Destination).Include(x => x.Status).Include(x => x.Package).FirstOrDefault(x => x.Id == id);
            return View(inquiry);
        }
    }
}