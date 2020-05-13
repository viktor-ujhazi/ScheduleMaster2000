using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleMaster.Models
{
    public class ScheduleModel
    {
        public int ScheduleID { get; set; }
        public string Title { get; set; }
        public int NumOfDays { get; set; }
        public int UserID { get; set; }
        public bool IsPublic { get; set; }


        //ScheduleModel(string title, int time)
        //{
        //    Title = title;
        //    Time = time;
        //}
    }
}
