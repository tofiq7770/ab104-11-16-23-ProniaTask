using Microsoft.AspNetCore.Mvc;

namespace ProniaAb104.Areas.ProniaAdmin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
