using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Financial.Controllers
{
    [Area("Financial")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
