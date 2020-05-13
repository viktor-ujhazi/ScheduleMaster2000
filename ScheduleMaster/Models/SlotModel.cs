using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleMaster.Models
{
    public class SlotModel
    {
        public int SlotID { get; set; }
        public int ScheduleID { get; set; }
        public int DayID { get; set; }
        public int TaskID { get; set; }
        public int StartSlot { get; set; }
        public int SlotLength { get; set; }

    }
}
