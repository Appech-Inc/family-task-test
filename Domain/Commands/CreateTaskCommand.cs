using System;

namespace Domain.Commands
{
    public class CreateTaskCommand
    {
        public string Subject { get; set; }
        public Nullable<Guid> AssignedMemberId { get; set; }
    }
}
