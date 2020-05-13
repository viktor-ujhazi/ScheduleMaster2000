using ScheduleMaster.Models;
using System.Collections.Generic;

namespace ScheduleMaster.Services
{
    public interface ISchedulesService
    {
        void AddSchedule(string title, int numOfColumns, int userID);
        void DeleteSchedule(int userID, int scheduleId);
        List<ScheduleModel> GetAllSchedule(int userID);
        ScheduleModel GetSchedule(int id);
        void UpdateSchedule(int schedule_id, string title, int numOfColumns, int userID, bool isPublic);
    }
}