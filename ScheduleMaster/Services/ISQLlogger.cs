﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleMaster.Services
{
    public interface ISQLlogger
    {
        public void Log(int userId, string actionName);
    }
}
