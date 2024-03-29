﻿using System;

namespace BugTracker.Model.Entities
{
    //класс для вывода в таблицу задач (с текстовыми полями для типа, статуса)
    public class TaskToView
    {
        private readonly string[] _taskTypes = { "Bug", "New feature", "Task" };

        private readonly string[] _taskStatus = { "Не назначена", "В работе", "Закончена" };

        public int Id { get; set; }
        public string Name { get; set; }
        public String Type { get; set; }
        public DateTime Date { get; set; }
        public String Status { get; set; }
        public string Comment { get; set; }
        public int DeveloperId { get; set; }

        //конструктор для создания "отображаемого" объекта задачи из объекта модели
        public TaskToView (Task task)
        {
            Id = task.Id;
            Name = task.Name;
            Date = task.Date;
            Comment = task.Comment;

            switch (task.Type)
            {
                case 0:
                    Type = _taskTypes[0];
                    break;
                case 1:
                    Type = _taskTypes[1];
                    break;
                case 2:
                    Type = _taskTypes[2];
                    break;
            }

            switch (task.Status)
            {
                case 0:
                    Status = _taskStatus[0];
                    break;
                case 1:
                    Status = _taskStatus[1];
                    break;
                case 2:
                    Status = _taskStatus[2];
                    break;
            }

            DeveloperId = task.DeveloperId;

        }
    }
}
