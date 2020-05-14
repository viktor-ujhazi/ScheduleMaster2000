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
            return resultJson;
        }

        public ActionResult UpdateSchedule()
        {
            int scheduleId = Convert.ToInt32(Request.Form["scheduleID"]);
            string title = Request.Form["title"];
            int numOfDays = Convert.ToInt32(Request.Form["numOfDays"]);
            int userID = Convert.ToInt32(Request.Form["userID"]);
            bool isPublic = Convert.ToBoolean(Request.Form["isPublic"]);



            _sqlScheduleService.UpdateSchedule(scheduleId, title, numOfDays, userID, isPublic);


            var resultJson = Json(_sqlScheduleService.GetAllSchedule(Convert.ToInt32(Request.Form["userid"])));
            return resultJson;
        }
    }
}