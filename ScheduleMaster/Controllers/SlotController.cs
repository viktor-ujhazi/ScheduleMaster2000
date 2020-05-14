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

            Dictionary<int, List<SlotModel>> markedSlots = new Dictionary<int, List<SlotModel>>();

            foreach (var dayId in daysIds)
            {
                markedSlots[Convert.ToInt32(dayId)] = _slotService.GetAllSlotByDayId(Convert.ToInt32(dayId));
            }
            var asd = JsonConvert.SerializeObject(markedSlots);
            return Json(asd);
        }
    }
}