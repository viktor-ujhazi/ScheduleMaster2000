using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleMaster.Services
{
    public interface IUsersService
    {
        public void AddUser(string name, string email, string password);
        public int GetUserId(string email);
    }
}
