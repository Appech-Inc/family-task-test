using System;

namespace Domain.Commands
{
    public class CreateTaskCommand
    {
        public string Subject { get; set; }
        public bool IsComplete { get; set; }
        public Guid? AssignedToId { get; set; }
    }
}