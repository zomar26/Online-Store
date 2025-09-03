using Microsoft.AspNetCore.Mvc;

namespace OnlineSupermarket.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
