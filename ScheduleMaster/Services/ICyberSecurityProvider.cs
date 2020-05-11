using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleMaster.Services
{
    public interface ICyberSecurityProvider
    {
        public string EncryptPassword(string password);
        public bool IsValidUser(string username, string password);
    }
}