using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleMaster.Models
{
    public class ScheduleModel
    {
        string Title { get; set; }
        int Time { get; set; }

        ScheduleModel(string title, int time)
        {
            Title = title;
            Time = time;
        }
    }
}
