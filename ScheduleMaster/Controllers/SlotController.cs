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
using Newtonsoft.Json;

namespace ScheduleMaster.Controllers
{
    public class SlotController : Controller
    {
        private readonly ISlotsService _slotService;
        private readonly ISQLlogger _sqlLogger;
        public SlotController(ISlotsService slotService, ISQLlogger sqlLogger)
        {
            _slotService = slotService;
            _sqlLogger = sqlLogger;
        }
        public IActionResult Index()
        {
            string dayIdsUncutted = Request.Form["dayIds"];
            string[] daysIds = dayIdsUncutted.Split(",");

            List<List<SlotModel>> markedSlots = new List<List<SlotModel>>();

            foreach (var dayId in daysIds)
            {
                markedSlots.Add(_slotService.GetAllSlotByDayId(Convert.ToInt32(dayId)));
            }

            return Json(markedSlots);
        }

        public IActionResult TaskToSlot()
        {

            var scheduleId = Convert.ToInt32(Request.Form["scheduleId"]);
            var dayId = Convert.ToInt32(Request.Form["dayId"]);
            var startSlot = Convert.ToInt32(Request.Form["startSlot"]);


            SlotTaskModel taskResult = null;
            try
            {
                taskResult = _slotService.GetTaskForSlot(scheduleId, dayId, startSlot);

            }
            catch (Exception)
            {

                return Json(taskResult);
            }
            return Json(taskResult);
        }

        public void DeleteSlot()
        {
            int userId = int.Parse(HttpContext.User.FindFirstValue("Id"));
            var slotId = Convert.ToInt32(Request.Form["slotId"]);

            _sqlLogger.Log(userId, $"Slot number {slotId} deleted");
            _slotService.DeleteSlot(slotId);
        }


        public IActionResult AddTask()
        {

            try
            {
                var scheduleId = Convert.ToInt32(Request.Form["scheduleId"]);
                var dayId = Convert.ToInt32(Request.Form["dayId"]);
                var taskId = Convert.ToInt32(Request.Form["taskId"]);
                var startSlot = Convert.ToInt32(Request.Form["startTime"]);
                var slotLength = Convert.ToInt32(Request.Form["slotLength"]);
                int userId = int.Parse(HttpContext.User.FindFirstValue("Id"));

                _slotService.AddSlot(scheduleId, dayId, taskId, startSlot, slotLength);
                _sqlLogger.Log(userId, $"TaskId {taskId}, added to schedule {scheduleId}, Day {dayId}, hour {startSlot}.");
                return Json("OK");
            }
            catch (Exception)
            {

                return Json("ERROR");
            }
        }
    }
}