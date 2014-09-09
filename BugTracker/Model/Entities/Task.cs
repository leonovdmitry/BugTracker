using System;

namespace BugTracker.Model.Entities
{
    //класс сущность задач
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
        public string Comment { get; set; }
        public int DeveloperId { get; set; }

        //конструктор для создания объекта модели из "отображаемого" объекта
        public Task(TaskToView task)
        {
            Id = task.Id;
            Name = task.Name;
            Date = task.Date;
            Comment = task.Comment;

            switch (task.Type)
            {
                case "Bug":
                    Type = 0;
                    break;
                case "New feature":
                    Type = 1;
                    break;
                case "Task":
                    Type = 2;
                    break;
            }

            switch (task.Status)
            {
                case "Не назначена":
                    Status = 0;
                    break;
                case "В работе":
                    Status = 1;
                    break;
                case "Закончена":
                    Status = 2;
                    break;
            }

            DeveloperId = task.DeveloperId;
        }

        public Task()
        {

        }
    }
}
