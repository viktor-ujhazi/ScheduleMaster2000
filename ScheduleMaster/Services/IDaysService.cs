using ScheduleMaster.Models;
using System.Collections.Generic;

namespace ScheduleMaster.Services
{
    public interface IDaysService
    {
        void DeleteDay(int userID, int dayId);
        List<DayModel> GetAllDay(int scheduleID);
        DayModel GetDay(int id);
        void UpdateDay(int dayId, string title);
    }
}