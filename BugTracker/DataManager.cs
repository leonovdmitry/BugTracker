using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTracker.DB;
using Microsoft.Win32;
using Task = BugTracker.DB.Task;

namespace BugTracker
{
    class DataManager
    {
        private BugsEntities _db = null;
        private BugsEntities Db
        {
            get { return _db ?? (_db = new BugsEntities()); }
        }

        public List<Developer> GetDevelopers()
        {
            return Db.Developer.ToList();
        }

        public List<Task> GetTasks()
        {
            return Db.Task.ToList();
        }

        public Developer GetDeveloper(int id)
        {
            return Db.Developer.SingleOrDefault(developer => developer.Id == id);
        }

        public Task GetTask(int id)
        {
            return Db.Task.SingleOrDefault(task => task.Id == id);
        }

        public List<Task> GetTasksByDeveloper(Developer developer)
        {
            return Db.Task.Where(task => task.DeveloperId == developer.Id).ToList();
        }

        public void SaveDeveloper(Developer developer)
        {
            var _developer = GetDeveloper(developer.Id);
            _developer.Name = developer.Name;
            _developer.Telefone = developer.Telefone;
            Db.SaveChanges();
        }

        public void SaveTask(Task task)
        {
            var _task = GetTask(task.Id);
            _task.Date = task.Date;
            _task.Type = task.Type;
            _task.Name = task.Name;
            _task.Comment = task.Comment;
            _task.Status = task.Status;
            _task.DeveloperId = task.DeveloperId;
            Db.SaveChanges();
        }

        public void AddDeveloper(Developer developer)
        {
            Db.Developer.Add(developer);
            Db.SaveChanges();
        }

        public void AddTask(Task task)
        {
            Db.Task.Add(task);
            Db.SaveChanges();
        }

        public void RemoveDeveloper(int id)
        {
            foreach (var task in GetTasks().Where(task=> task.DeveloperId == id))
                task.DeveloperId = null;
            
            Db.Developer.Remove(Db.Developer.SingleOrDefault(developer => developer.Id == id));
            Db.SaveChanges();
        }

        public void RemoveTask(int id)
        {
            Db.Task.Remove(Db.Task.SingleOrDefault(task => task.Id == id));
            Db.SaveChanges();
        }

    }
}
