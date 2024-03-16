using Microsoft.AspNetCore.Mvc;
using WebCourierHub.Classes;
using WebCourierHub.Data;
using WebCourierHub.Models;

namespace WebCourierHub.Controllers.Api
{
    [Route("api/Company")]
    [ApiController]
    public class CompanyApiController : Controller
    {
        private readonly WebCourierHubDbContext _context;

        public CompanyApiController(WebCourierHubDbContext context)
        {
            _context = context;
        }

        [HttpPost("Add")]
        public IActionResult Add(CompanyDto companyDto)
        {
            var company = Mapper.MapCompanyDtoToModel(companyDto);

            _context.Add(company);
            _context.SaveChanges();

            return Ok("Company has been added");
        }
    }
}