using ScheduleMaster.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleMaster.Services
{
    public class SQLDaysService : IDaysService
    {
        private readonly IDbConnection _connection;

        public SQLDaysService(IDbConnection connection)
        {
            _connection = connection;
        }

        private DayModel DayModelFromData(IDataReader reader)
        {
            return new DayModel
            {
                DayID = (int)reader["day_id"],
                ScheduleID = (int)reader["schedule_id"],
                Title = (string)reader["title"],

            };
        }

        public DayModel GetDay(int id)
        {
            using var command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM days WHERE day_id = @day_id";

            var param = command.CreateParameter();
            param.ParameterName = "day_id";
            param.Value = id;

            command.Parameters.Add(param);

            using var reader = command.ExecuteReader();
            reader.Read();
            return DayModelFromData(reader);
        }

        public List<DayModel> GetAllDay(int scheduleID)
        {
            using var command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM days WHERE schedule_id = @schedule_id ORDER BY day_id";

            var param = command.CreateParameter();
            param.ParameterName = "schedule_id";
            param.Value = scheduleID;

            command.Parameters.Add(param);

            using var reader = command.ExecuteReader();

            List<DayModel> days = new List<DayModel>();
            while (reader.Read())
            {
                days.Add(DayModelFromData(reader));
            }
            return days;

        }

        public void UpdateDay(int dayId, string title)
        {
            using var command = _connection.CreateCommand();

            var dayIdParam = command.CreateParameter();
            dayIdParam.ParameterName = "day_id";
            dayIdParam.Value = dayId;

            var titleParam = command.CreateParameter();
            titleParam.ParameterName = "title";
            titleParam.Value = (object)title ?? DBNull.Value;

            command.CommandText = "UPDATE days SET title = @title WHERE day_id = @day_id";

            command.Parameters.Add(titleParam);
            command.Parameters.Add(dayIdParam);


            command.ExecuteNonQuery();
        }
        public void DeleteDay(int userID, int dayId)
        {
            using var command = _connection.CreateCommand();

            var userIdParam = command.CreateParameter();
            userIdParam.ParameterName = "user_id";
            userIdParam.Value = userID;

            var dayIdParam = command.CreateParameter();
            dayIdParam.ParameterName = "day_id";
            dayIdParam.Value = dayId;

            command.CommandText = "DELETE FROM days WHERE day_id= @day_id)";
            //command.Parameters.Add(userIdParam);
            command.Parameters.Add(dayIdParam);

            command.ExecuteNonQuery();
        }
    }
}
