using System;
using ScheduleMaster.Services;
using Microsoft.AspNetCore.Mvc;


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

        public void UpdateTitle()
        {
            _daysService.UpdateDay(Convert.ToInt32(Request.Form["dayId"]), Request.Form["title"]);

        }
    }
}