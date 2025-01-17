using BookStore.Models;
using BookStore.Repositary;
using BookStore.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
        public readonly NewBookAlertConfig _newbookAlertconfiguration;
        public readonly NewBookAlertConfig _thirdPartyBookconfiguration;
        public readonly IMessageRepositary _messageRepositary;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        public HomeController(ILogger<HomeController> logger, IOptionsSnapshot<NewBookAlertConfig> newbookAlertconfiguration,
            IMessageRepositary messageRepositary, IUserService userService, IEmailService emailService)
        {
            _logger = logger;
            _newbookAlertconfiguration = newbookAlertconfiguration.Get("InternalBook");
            _thirdPartyBookconfiguration = newbookAlertconfiguration.Get("ThirdPartyBook");
            _messageRepositary = messageRepositary;
            _userService = userService;
            _emailService = emailService;
        }
        [ViewData]
        public string Title { get; set; }
        public async Task<IActionResult> Index()
        {
            //var userId = _userService.GetUserId();
            //var IsLoggedIn = _userService.IsAuthenticated();
            //UserEmailOptions options = new UserEmailOptions
            //{
            //    ToEmails = new List<string>() { "gauravparange@gmail.com" },
            //    PlaceHolders = new List<KeyValuePair<string, string>>()
            //    {
            //        new KeyValuePair<string, string>("{{UserName}}","Nitesh")
            //    }

            //};
            //await _emailService.SendTestEmail(options);

            //ViewBag.Title = "Home";
            //ViewBag.Id = 1;

            //dynamic data = new ExpandoObject();
            //data.Title = "Title";
            //data.Id = 2;
            //ViewBag.data = data;
            //Title = "HomePage";
            ////ViewBag.type = new BookModel() { Id = 3, Title = "Type" };

            //ViewData["MyProperty"] = "Home";
            //ViewData["book"] = new BookModel() { Id = 1, Author = "Gaurav" };

            var value = _messageRepositary.GetName();

            return View();
        }
        public IActionResult AboutUs()
        {
            Title = "About Us Page";
            return View();
        }
        [Authorize(Roles ="Admin")]
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
