using Domain.ViewModel;
using System;

namespace Domain.Commands
{
    public class CreateTaskCommandResult
    {
        public TaskVm Payload { get; set; }
    }
}
