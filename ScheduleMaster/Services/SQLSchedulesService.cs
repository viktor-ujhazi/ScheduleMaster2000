using ScheduleMaster.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleMaster.Services
{
    public class SQLSchedulesService : ISchedulesService
    {
        private readonly IDbConnection _connection;

        public SQLSchedulesService(IDbConnection connection)
        {
            _connection = connection;
        }

        private ScheduleModel ScheduleModelFromData(IDataReader reader)
        {
            return new ScheduleModel
            {
                ScheduleID = (int)reader["schedule_id"],
                Title = (string)reader["title"],
                NumOfDays = (int)reader["num_of_days"],
                UserID = (int)reader["user_id"],
                IsPublic = (bool)reader["is_public"]
            };
        }



        public void AddSchedule(string title, int numOfDays, int userID)
        {
            using var command = _connection.CreateCommand();

            var titleParam = command.CreateParameter();
            titleParam.ParameterName = "title";
            titleParam.Value = (object)title ?? DBNull.Value;

            var numOfDaysParam = command.CreateParameter();
            numOfDaysParam.ParameterName = "num_of_days";
            numOfDaysParam.Value = numOfDays;

            var userIDParam = command.CreateParameter();
            userIDParam.ParameterName = "user_id";
            userIDParam.Value = userID;

            command.CommandText = "SELECT insert_into_schedule(@title, @num_of_days, @user_id)";
            command.Parameters.Add(titleParam);
            command.Parameters.Add(numOfDaysParam);
            command.Parameters.Add(userIDParam);

            command.ExecuteNonQuery();

        }
        public ScheduleModel GetSchedule(int id)
        {
            using var command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM schedules WHERE schedules_id = @schedules_id";

            var param = command.CreateParameter();
            param.ParameterName = "schedules_id";
            param.Value = id;

            command.Parameters.Add(param);
            using var reader = command.ExecuteReader();
            reader.Read();
            return ScheduleModelFromData(reader);
        }

        public List<ScheduleModel> GetAllSchedule(int userID)
        {
            using var command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM schedules WHERE user_id = @user_id";
            
            var param = command.CreateParameter();
            param.ParameterName = "user_id";
            param.Value = userID;

            command.Parameters.Add(param);
            using var reader = command.ExecuteReader();

            List<ScheduleModel> schedules = new List<ScheduleModel>();
            while (reader.Read())
            {
                schedules.Add(ScheduleModelFromData(reader));
            }
            return schedules;

        }

        public void UpdateSchedule(int schedule_id, string title, int numOfDays, int userID, bool isPublic)
        {
            using var command = _connection.CreateCommand();

            var scheduleIdParam = command.CreateParameter();
            scheduleIdParam.ParameterName = "schedule_id";
            scheduleIdParam.Value = schedule_id;

            var titleParam = command.CreateParameter();
            titleParam.ParameterName = "title";
            titleParam.Value = (object)title ?? DBNull.Value;

            var numOfDaysParam = command.CreateParameter();
            numOfDaysParam.ParameterName = "num_of_days";
            numOfDaysParam.Value = numOfDays;

            var userIdParam = command.CreateParameter();
            userIdParam.ParameterName = "user_id";
            userIdParam.Value = userID;

            var isPublicParam = command.CreateParameter();
            isPublicParam.ParameterName = "is_public";
            isPublicParam.Value = isPublic;

            command.CommandText = "UPDATE schedules SET title = @title, num_of_days = @num_of_days, is_public = @is_public WHERE schedule_id = @schedule_id";

            command.Parameters.Add(scheduleIdParam);
            command.Parameters.Add(titleParam);
            command.Parameters.Add(numOfDaysParam);
            command.Parameters.Add(userIdParam);
            command.Parameters.Add(isPublicParam);

            command.ExecuteNonQuery();
        }
        public void DeleteSchedule(int userID, int scheduleId)
        {
            using var command = _connection.CreateCommand();

            var userIdParam = command.CreateParameter();
            userIdParam.ParameterName = "user_id";
            userIdParam.Value = userID;

            var scheduleidParam = command.CreateParameter();
            scheduleidParam.ParameterName = "schedule_id";
            scheduleidParam.Value = scheduleId;

            command.CommandText = "DELETE FROM schedules WHERE schedule_id= @schedule_id)";
            command.Parameters.Add(userIdParam);
            command.Parameters.Add(scheduleidParam);

            command.ExecuteNonQuery();
        }


    }
}
