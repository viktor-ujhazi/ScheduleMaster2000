using System;
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
            var resultJson = Json(_sqlTaskService.GetAllTask(Convert.ToInt32(Request.Form["userid"])));
            return resultJson;
        }

        public IActionResult TaskHandler()
        {
            var taskId = Request.Form["taskid"];

            return Json(_sqlTaskService.GetTask(Convert.ToInt32(taskId)));
        }

        public IActionResult AddTask()
        {
            var title = Request.Form["title"];
            var content = Request.Form["content"];
            var userId = Convert.ToInt32(Request.Form["userID"]);

            _sqlTaskService.AddTask(title, content, userId);

            return Json(_sqlTaskService.GetAllTask(userId));
        }

        public ActionResult UpdateTask()
        {

            int taskId = Convert.ToInt32(Request.Form["taskID"]);
            string title = Request.Form["title"];
            string content = Request.Form["content"];
            int userID = Convert.ToInt32(Request.Form["userID"]);


            _sqlTaskService.UpdateTask(taskId, title, content, userID);


            var resultJson = Json(_sqlTaskService.GetAllTask(Convert.ToInt32(Request.Form["userid"])));
            return resultJson;
        }


    }
}