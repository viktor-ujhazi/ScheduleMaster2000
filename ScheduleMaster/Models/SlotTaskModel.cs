using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleMaster.Models
{
    public class SlotTaskModel
    {
        public TaskModel TaskModel_ { get; set; }
        public int SlotLength { get; set; }
    }
}
