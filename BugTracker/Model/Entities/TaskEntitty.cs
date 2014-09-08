using System;

namespace BugTracker.Model.Entities
{
    public class TaskEntitty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
        public string Comment { get; set; }
        public int DeveloperId { get; set; }
    }
}
