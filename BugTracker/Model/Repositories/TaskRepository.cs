using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using BugTracker.Model.Entities;

namespace BugTracker.Model.Repositories
{
    //репозиторий для объетов task 
    public class TaskRepository
    {
        private readonly SqlConnection _connection;

        public DataManager DataManager { get; private set; }

        public TaskRepository(SqlConnection sqlConnect, DataManager dataManager)
        {
            _connection = sqlConnect;
            DataManager = dataManager;

        }

        ////получить список всех задач
        public List<Task> GetList()
        {
            var querry = new SqlCommand("select * from Task", _connection);
            var taskList = new List<Task>();
            using (var dataReader = querry.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    taskList.Add(
                        new Task
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

        //преобразование списка задач к "отображаемому" списку
        public List<TaskToView> TasksToView(List<Task> tasks)
        {
            var taskList = tasks.Select(taskEntitty => new TaskToView(taskEntitty)).ToList();
            return taskList;
        }

        //преобразование "отображаемого" списка к списку задач  
        public List<Task> TaskViewToEntity(List<TaskToView> tasks)
        {
            return tasks.Select(taskToView => new Task(taskToView)).ToList();
        }

        //определение: добавление или редактирование задачи
        public void AddOrEditTask(Task task)
        {
            if (task.Id == 0)
                Add(task);
            else
                Edit(task);
        }

        //удаление
        public void Delete(Task task)
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

        //добавлние в базе
        private void Add(Task task)
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

        //редактирование в базе
        private void Edit(Task task)
        {
            var query =
                string.Format(
                    "update Task set Date = '{0}', Type = '{1}', Name = '{2}', Comment = '{3}', Status = '{4}', DeveloperId = '{5}' where Id = '{6}'",
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
