using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleMaster.Services
{
    public class SQLUsersService : IUsersService
    {
        private readonly IDbConnection _connection;

        public SQLUsersService(IDbConnection connection)
        {
            _connection = connection;
        }
        public void AddUser(string name, string email, string password)
        {
            using var command = _connection.CreateCommand();

            var usernameParam = command.CreateParameter();
            usernameParam.ParameterName = "username";
            usernameParam.Value = name;

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

            command.ExecuteNonQuery();
        }
    }
}
