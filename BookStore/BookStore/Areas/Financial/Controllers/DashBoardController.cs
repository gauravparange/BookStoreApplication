using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Financial.Controllers
{
    [Area("Financial")]
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Graph()
        {
            return View();
        }
    }
}
