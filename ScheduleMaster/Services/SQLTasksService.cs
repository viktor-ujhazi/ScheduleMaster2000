using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ScheduleMaster.Models;

namespace ScheduleMaster.Services
{
    public class SQLTasksService : ITasksService
    {
        private readonly IDbConnection _connection;

        public SQLTasksService(IDbConnection connection)
        {
            _connection = connection;
        }

        private TaskModel TaskModelFromData(IDataReader reader)
        {
            return new TaskModel
            {
                TaskID = (int)reader["task_id"],
                Title = (string)reader["title"],
                Content = (string)reader["content"],
                UserID = (int)reader["user_id"]
            };
        }



        public void AddTask(string title, string content, int userID)
        {
            using var command = _connection.CreateCommand();

            var titleParam = command.CreateParameter();
            titleParam.ParameterName = "title";
            titleParam.Value = (object)title ?? DBNull.Value;

            var contentParam = command.CreateParameter();
            contentParam.ParameterName = "content";
            contentParam.Value = (object)content ?? DBNull.Value;

            var userIDParam = command.CreateParameter();
            userIDParam.ParameterName = "user_id";
            userIDParam.Value = userID;

            command.CommandText = @"INSERT INTO tasks (title, content, user_id) VALUES (@title, @content, @user_id)";
            command.Parameters.Add(titleParam);
            command.Parameters.Add(contentParam);
            command.Parameters.Add(userIDParam);

            command.ExecuteNonQuery();
        }
        public TaskModel GetTask(int id)
        {
            using var command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM tasks WHERE task_id = @task_id";

            var param = command.CreateParameter();
            param.ParameterName = "task_id";
            param.Value = id;

            command.Parameters.Add(param);
            using var reader = command.ExecuteReader();
            reader.Read();
            return TaskModelFromData(reader);
        }

        public List<TaskModel> GetAllTask(int userID)
        {
            using var command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM tasks WHERE user_id = @user_id";

            var param = command.CreateParameter();
            param.ParameterName = "user_id";
            param.Value = userID;

            command.Parameters.Add(param);
            using var reader = command.ExecuteReader();

            List<TaskModel> tasks = new List<TaskModel>();
            while (reader.Read())
            {
                tasks.Add(TaskModelFromData(reader));
            }
            return tasks;

        }

        public void UpdateTask(int taskID, string title, string content, int userID)
        {
            using var command = _connection.CreateCommand();

            var taskIdParam = command.CreateParameter();
            taskIdParam.ParameterName = "task_id";
            taskIdParam.Value = taskID;

            var titleParam = command.CreateParameter();
            titleParam.ParameterName = "title";
            titleParam.Value = (object)title ?? DBNull.Value;

            var contentParam = command.CreateParameter();
            contentParam.ParameterName = "content";
            contentParam.Value = (object)content ?? DBNull.Value;

            var userIdParam = command.CreateParameter();
            userIdParam.ParameterName = "user_id";
            userIdParam.Value = userID;

            command.CommandText = "UPDATE tasks SET title = @title, content = @content WHERE task_id = @task_id";

            command.Parameters.Add(taskIdParam);
            command.Parameters.Add(titleParam);
            command.Parameters.Add(contentParam);
            command.Parameters.Add(userIdParam);
            command.ExecuteNonQuery();
        }
        public void DeleteTask(int userID, int taskID)
        {
            using var command = _connection.CreateCommand();

            var userIdParam = command.CreateParameter();
            userIdParam.ParameterName = "user_id";
            userIdParam.Value = userID;

            var taskidParam = command.CreateParameter();
            taskidParam.ParameterName = "task_id";
            taskidParam.Value = taskID;

            command.CommandText = "DELETE FROM tasks WHERE task_id= @task_id)";
            command.Parameters.Add(userIdParam);
            command.Parameters.Add(taskidParam);

            command.ExecuteNonQuery();
        }



    }
}
