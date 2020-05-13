using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScheduleMaster.Models;
using ScheduleMaster.Services;

namespace ScheduleMaster.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly IUsersService _userservice;

        public HomeController(ILogger<HomeController> logger) //IUsersService usersService)
        {
            _logger = logger;
            //_userservice = usersService;
        }

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
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult MyAjaxGET()
        {
            string temp = Request.Query["username"];

            // Perform your operation  

            return Json(temp);
        }
    }
}
