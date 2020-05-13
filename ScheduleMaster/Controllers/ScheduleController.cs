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
    public class ScheduleController : Controller
    {
        private readonly ISchedulesService _sqlScheduleService;

        public ScheduleController(ISchedulesService sqlScheduleService)
        {
            _sqlScheduleService = sqlScheduleService;
        }
        public ActionResult Index()
        {
            var resultJson = Json(_sqlScheduleService.GetAllSchedule(Convert.ToInt32(Request.Form["userid"])));
            return Json(resultJson);
        }
    }
}