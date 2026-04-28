using Microsoft.AspNetCore.Mvc;

namespace BlindMatchPAS.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                if (User.IsInRole("Student")) return RedirectToAction("Dashboard", "Student");
                if (User.IsInRole("Supervisor")) return RedirectToAction("Dashboard", "Supervisor");
                if (User.IsInRole("ModuleLeader")) return RedirectToAction("Dashboard", "ModuleLeader");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
