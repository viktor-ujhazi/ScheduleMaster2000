using System;
using ScheduleMaster.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ScheduleMaster.Controllers
{
    public class DayController : Controller
    {
        private readonly IDaysService _daysService;
        private readonly ISQLlogger _sqlLogger;

        public DayController(IDaysService daysService, ISQLlogger sqlLogger)
        {
            _daysService = daysService;
            _sqlLogger = sqlLogger;
        }
        public IActionResult Index()
        {
            int userId = int.Parse(HttpContext.User.FindFirstValue("Id"));
            var scheduleId = Convert.ToInt32(Request.Form["scheduleId"]);

            _sqlLogger.Log(userId, $"Opened Schedule Id: {scheduleId} ");
            var resultJson = Json(_daysService.GetAllDay(scheduleId));
            return resultJson;
        }

        public void UpdateTitle()
        {
            int userId = int.Parse(HttpContext.User.FindFirstValue("Id"));
            _sqlLogger.Log(userId, $"Day {Request.Form["dayId"]}: title update to {Request.Form["title"]}");
            _daysService.UpdateDay(Convert.ToInt32(Request.Form["dayId"]), Request.Form["title"]);

        }
    }
}