using Microsoft.AspNetCore.Mvc;

namespace WebCourierHub.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.User = User.Identity.Name;
            return View();
        }
    }
}