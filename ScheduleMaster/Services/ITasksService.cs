using ScheduleMaster.Models;
using System.Collections.Generic;

namespace ScheduleMaster.Services
{
    public interface ITasksService
    {
        void AddTask(string title, string content, int userID);
        void DeleteTask(int userID, int taskID);
        List<TaskModel> GetAllTask(int userID);
        TaskModel GetTask(int id);
        void UpdateTask(int taskID, string title, string content, int userID);
    }
}