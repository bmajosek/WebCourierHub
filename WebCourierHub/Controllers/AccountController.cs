using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebCourierHub.Classes;
using WebCourierHub.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebCourierHub.Models;

namespace WebCourierHub.Controllers
{
    public class AccountController : Controller
    {
        private readonly WebCourierHubDbContext _context;

        public AccountController(WebCourierHubDbContext context)
        {
            _context = context;
        }

        public async Task Login(string returnUrl = "/")
        {
            var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
                // Indicate here where Auth0 should redirect the user after a login.
                // Note that the resulting absolute Uri must be added to the
                // **Allowed Callback URLs** settings for the app.
                .WithRedirectUri(returnUrl)
                .Build();

            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        }

        [Authorize]
        public IActionResult Profile()
        {
            var userProfileTmp = _context.User.FirstOrDefault(x => x.IdentityName == User.Identity.Name);
            if (userProfileTmp == null)
            {
                var newUser = new User() { IdentityName = User.Identity.Name };
                _context.Add(newUser);
                _context.SaveChanges();
            }
            var userProfile = new UserProfileDto()
            {
                Name = User.Identity.Name,
                EmailAddress = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                ProfileImage = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value,
                Role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? "Client",
                Company = _context.User.FirstOrDefault(x => x.IdentityName == User.Identity.Name).Company,
                Companies = _context.Company
                           .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name })
                           .ToList()
            };

            return View(userProfile);
        }

        [Authorize] // tu jest problem że nie dało się wejść bo nie byłem zautoryzowany
        public async Task Logout()
        {
            var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
                // Indicate here where Auth0 should redirect the user after a logout.
                // Note that the resulting absolute Uri must be added to the
                // **Allowed Logout URLs** settings for the app.
                .WithRedirectUri(Url.Action("Index", "Home"))
                .Build();

            await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        [Authorize]
        [HttpPost("SetCompany/{companyId}")]
        public IActionResult SetCompany(int companyId)
        {
            var userProfile = _context.User.FirstOrDefault(x => x.IdentityName == User.Identity.Name);
            userProfile.Company = _context.Company.FirstOrDefault(x => x.Id == companyId);
            _context.SaveChanges();
            return Ok("Company has been changed");
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}