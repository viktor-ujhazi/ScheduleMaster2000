﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleMaster.Models
{
    public class DayModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int UserID { get; set; }
    }
}
