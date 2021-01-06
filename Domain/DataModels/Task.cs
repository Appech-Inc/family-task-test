using System;

namespace Domain.DataModels
{
    public class Task
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Subject { get; set; }
        public bool IsComplete { get; set; }
        public Guid? AssignedToId { get; set; }
    }
}