﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScheduleMaster.Services;

namespace ScheduleMaster.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITasksService _sqlTaskService;

        public TaskController(ITasksService sqlTaskService)
        {
            _sqlTaskService = sqlTaskService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}