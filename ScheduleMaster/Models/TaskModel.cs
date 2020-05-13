using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleMaster.Models
{
    public class TaskModel
    {
        public int TaskID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int UserID { get; set; }

        //public TaskModel(int taskID, string title, string content, int userID)
        //{
        //    TaskID = taskID;
        //    Title = title;
        //    Content = content;
        //    UserID = userID;
        //}
        
    }
}
