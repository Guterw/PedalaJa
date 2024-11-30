using Microsoft.AspNetCore.Mvc;

namespace SeuProjeto.Controllers
{
    public class LandingPageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}


