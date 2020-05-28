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
            userIdParam.ParameterName = "username";
            userIdParam.Value = name;

            var emailParam = command.CreateParameter();
            emailParam.ParameterName = "email";
            emailParam.Value = email;

            var passwordParam = command.CreateParameter();
            passwordParam.ParameterName = "password";
            passwordParam.Value = password;

            command.CommandText = @"INSERT INTO users (username, password, email) VALUES (@username, @password, @email)";
            command.Parameters.Add(usernameParam);
            command.Parameters.Add(passwordParam);
            command.Parameters.Add(emailParam);

            command.ExecuteNonQuery(); ;
        }
    }
}
