using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ScheduleMaster.Models;
using ScheduleMaster.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace ScheduleMaster.Controllers
{
    public class DayController : Controller
    {
        private readonly IDaysService _daysService;

        public DayController(IDaysService daysService)
        {
            _daysService = daysService;
        }
        public IActionResult Index()
        {
            var resultJson = Json(_daysService.GetAllDay(Convert.ToInt32(Request.Form["scheduleId"])));
            return resultJson;
        }
    }
}