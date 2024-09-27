using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [ViewData]
        public string Title { get; set; }
        public IActionResult Index()
        {
            //ViewBag.Title = "Home";
            //ViewBag.Id = 1;

            dynamic data = new ExpandoObject();
            data.Title = "Title";
            data.Id = 2;
            ViewBag.data = data;
            Title = "HomePage";
            //ViewBag.type = new BookModel() { Id = 3, Title = "Type" };

            ViewData["MyProperty"] = "Home";
            ViewData["book"] = new BookModel() { Id = 1, Author = "Gaurav" };
            return View();
        }
        public IActionResult AboutUs()
        {
            Title = "About Us Page";
            return View();
        }
        
        public IActionResult ContactUs()
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
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
