using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleMaster.Services
{
    public class SQLLogger : ISQLlogger
    {

        private readonly IDbConnection _connection;

        public SQLLogger(IDbConnection connection)
        {
            _connection = connection;
        }
        public void Log(int userId, string actionName)
        {
            using var command = _connection.CreateCommand();

            var userIdParam = command.CreateParameter();
            userIdParam.ParameterName = "user_id";
            userIdParam.Value = userId;

            var actionParam = command.CreateParameter();
            actionParam.ParameterName = "action_text";
            actionParam.Value = actionName;


            command.CommandText = @"INSERT INTO logs (user_id, action_text) VALUES (@user_id, @action_text)";
            command.Parameters.Add(userIdParam);
            command.Parameters.Add(actionParam);


            command.ExecuteNonQuery(); ;
        }
    }
}
