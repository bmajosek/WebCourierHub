using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebCourierHub.Classes;

using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Authentication.Cookies;

namespace WebCourierHub.Controllers
{
    public class ClientController : Controller
    {
        public ClientController()
        { }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}