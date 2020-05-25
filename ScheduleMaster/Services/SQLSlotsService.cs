using ScheduleMaster.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleMaster.Services
{
    public class SQLSlotsService : ISlotsService
    {
        private readonly IDbConnection _connection;

        public SQLSlotsService(IDbConnection connection)
        {
            _connection = connection;
        }

        private SlotModel SlotModelFromData(IDataReader reader)
        {
            return new SlotModel
            {
                SlotID = (int)reader["slot_id"],
                ScheduleID = (int)reader["schedule_id"],
                DayID = (int)reader["day_id"],
                TaskID = (int)reader["task_id"],
                StartSlot = (int)reader["start_slot"],
                SlotLength = (int)reader["slot_length"]

            };
        }



        public void AddSlot(int scheduleID, int dayID, int taskID, int startSlot, int slotLength)
        {
            using var command = _connection.CreateCommand();

            var scheduleIDParam = command.CreateParameter();
            scheduleIDParam.ParameterName = "schedule_id";
            scheduleIDParam.Value = scheduleID;

            var dayIDParam = command.CreateParameter();
            dayIDParam.ParameterName = "day_id";
            dayIDParam.Value = dayID;

            var taskIDParam = command.CreateParameter();
            taskIDParam.ParameterName = "task_id";
            taskIDParam.Value = taskID;

            var startSlotParam = command.CreateParameter();
            startSlotParam.ParameterName = "start_slot";
            startSlotParam.Value = startSlot;

            var slotLengthParam = command.CreateParameter();
            slotLengthParam.ParameterName = "slot_length";
            slotLengthParam.Value = slotLength;

            command.CommandText = @"INSERT INTO slots (schedule_id, day_id, task_id, start_slot, slot_length) VALUES (@schedule_id, @day_id, @task_id, @start_slot, @slot_length)";
            command.Parameters.Add(scheduleIDParam);
            command.Parameters.Add(dayIDParam);
            command.Parameters.Add(taskIDParam);
            command.Parameters.Add(startSlotParam);
            command.Parameters.Add(slotLengthParam);


            command.ExecuteNonQuery();
        }
        public SlotModel GetSlot(int id)
        {
            using var command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM slots WHERE slot_id = @slot_id";

            var param = command.CreateParameter();
            param.ParameterName = "slot_id";
            param.Value = id;

            command.Parameters.Add(param);

            using var reader = command.ExecuteReader();
            reader.Read();
            return SlotModelFromData(reader);
        }

        public List<SlotModel> GetAllSlot(int scheduleID)
        {
            using var command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM slots WHERE schedule_id = @schedule_id";

            var param = command.CreateParameter();
            param.ParameterName = "schedule_id";
            param.Value = scheduleID;

            command.Parameters.Add(param);

            using var reader = command.ExecuteReader();

            List<SlotModel> slots = new List<SlotModel>();
            while (reader.Read())
            {
                slots.Add(SlotModelFromData(reader));
            }
            return slots;

        }

        public void UpdateSlot(int slotID, int dayID, int taskID, int startSlot, int slotLength)
        {
            using var command = _connection.CreateCommand();

            var dayIDParam = command.CreateParameter();
            dayIDParam.ParameterName = "day_id";
            dayIDParam.Value = dayID;

            var taskIDParam = command.CreateParameter();
            taskIDParam.ParameterName = "task_id";
            taskIDParam.Value = taskID;

            var startSlotParam = command.CreateParameter();
            startSlotParam.ParameterName = "start_slot";
            startSlotParam.Value = startSlot;

            var slotLengthParam = command.CreateParameter();
            slotLengthParam.ParameterName = "slot_length";
            slotLengthParam.Value = slotLength;

            var slotIDParam = command.CreateParameter();
            slotIDParam.ParameterName = "slot_id";
            slotIDParam.Value = slotID;

            command.CommandText = "UPDATE slots SET day_id = @day_id, task_id = @task_id, start_slot = @start_slot, slot_length = @slot_length WHERE slot_id = @slot_id";

            command.Parameters.Add(dayIDParam);
            command.Parameters.Add(taskIDParam);
            command.Parameters.Add(startSlotParam);
            command.Parameters.Add(slotLengthParam);
            command.Parameters.Add(slotIDParam);

            command.ExecuteNonQuery();
        }
        public void DeleteSlot(int userID, int slotID)
        {
            using var command = _connection.CreateCommand();

            var userIdParam = command.CreateParameter();
            userIdParam.ParameterName = "user_id";
            userIdParam.Value = userID;

            var slotIDParam = command.CreateParameter();
            slotIDParam.ParameterName = "slot_id";
            slotIDParam.Value = slotID;

            command.CommandText = "DELETE FROM slots WHERE slot_id= @slot_id)";
            //command.Parameters.Add(userIdParam);
            command.Parameters.Add(slotIDParam);

            command.ExecuteNonQuery();
        }


        public List<SlotModel> GetAllSlotByDayId(int dayID)
        {
            using var command = _connection.CreateCommand();
            command.CommandText = $"SELECT * FROM slots WHERE day_id = {dayID}";

            using var reader = command.ExecuteReader();

            List<SlotModel> slots = new List<SlotModel>();
            while (reader.Read())
            {
                slots.Add(SlotModelFromData(reader));
            }
            return slots;
        }

        
        public SlotTaskModel GetTaskForSlot(int scheduleID, int dayID, int startSlot)
        {
            var commandT = _connection.CreateCommand();
            commandT.CommandText = $"(SELECT task_id, slot_length FROM slots WHERE day_id = {dayID} and schedule_id = {scheduleID} and start_slot = {startSlot}) ";
            var readerT = commandT.ExecuteReader();
            readerT.Read();
            int taskID = (int)readerT["task_id"];
            int slotLength = (int)readerT["slot_length"];
            commandT.Dispose();
            readerT.Dispose();

            using var command = _connection.CreateCommand();
            command.CommandText = $"SELECT * FROM tasks WHERE task_id = {taskID} ";
              

            using var reader = command.ExecuteReader();
            reader.Read();
            SlotTaskModel resultTask = new SlotTaskModel
            {
                TaskModel_ = new TaskModel{
                TaskID = (int)reader["task_id"],
                Title = (string)reader["title"],
                Content = (string)reader["content"],
                UserID = (int)reader["user_id"],
                },
                SlotLength = slotLength


            };


            return resultTask;
        }
    }
}
