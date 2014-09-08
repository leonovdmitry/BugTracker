using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BugTracker.Model.Entities;

namespace BugTracker.Model.Repositories
{
    public class TaskRepository
    {
        private readonly SqlConnection _connection;

        public DataManage DataManager { get; private set; }

        public TaskRepository(SqlConnection sqlConnect, DataManage dataManager)
        {
            _connection = sqlConnect;
            DataManager = dataManager;

        }

        public IEnumerable<TaskEntitty> GetList()
        {
            var querry = new SqlCommand("select * from Task", _connection);
            var taskList = new List<TaskEntitty>();
            using (var dataReader = querry.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    taskList.Add(
                        new TaskEntitty
                        {
                            Id = (int)dataReader["Id"],
                            Date = (DateTime)dataReader["Date"],
                            Type = (int)dataReader["Type"],
                            Name = dataReader["Name"].ToString(),
                            Comment = dataReader["Comment"].ToString(),
                            Status = (int)dataReader["Status"],
                            DeveloperId = (int)dataReader["DeveloperId"]
                        });
                }
            }

            return taskList;
        }

        public void AddOrEditTask(TaskEntitty task)
        {
            if (task.Id == 0)
                Add(task);
            else
                Edit(task);
        }

        public void Delete(TaskEntitty task)
        {
            var query = string.Format("delete from Task where Id = '{0}'", task.Id);
            var cmd = new SqlCommand(query, _connection);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка удаления!", ex);
            }

        }

        private void Add(TaskEntitty task)
        {
            var query =
                string.Format(
                    "insert into Task (Date, Type, Name, Comment, Status, DeveloperId) values" + "('{0}', '{1}','{2}', '{3}','{4}', '{5}')",
                    task.Date,
                    task.Type,
                    task.Name,
                    task.Comment,
                    task.Status,
                    task.DeveloperId);
            try
            {
                var cmd = new SqlCommand(query, _connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка добавления!", ex);
            }
        }

        private void Edit(TaskEntitty task)
        {
            var query =
                string.Format(
                    "update Task set Date = {0}, Type = {1}, Name = {2}, Comment = {3}, Status = {4}, DeveloperId = {5}) where Id = {6})",
                    task.Date,
                    task.Type,
                    task.Name,
                    task.Comment,
                    task.Status,
                    task.DeveloperId,
                    task.Id);
            try
            {
                var cmd = new SqlCommand(query, _connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка Изменения!", ex);
            }
        }

    }
}
