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

        public SlotController(ISlotsService slotService)
        {
            _slotService = slotService;
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
            var slotId = Convert.ToInt32(Request.Form["slotId"]);
            _slotService.DeleteSlot(slotId);
        }


        public void AddTask()
        {
            var scheduleId = Convert.ToInt32(Request.Form["scheduleId"]);
            var dayId = Convert.ToInt32(Request.Form["dayId"]);
            var taskId = Convert.ToInt32(Request.Form["taskId"]);
            var startSlot = Convert.ToInt32(Request.Form["startTime"]);
            var slotLength = Convert.ToInt32(Request.Form["slotLength"]);

            _slotService.AddSlot(scheduleId, dayId, taskId, startSlot, slotLength);
        }
    }
}