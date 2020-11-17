using System;
using Domain.ViewModel;

namespace WebClient.Shared.Models
{
    public class TaskModel
    {
        public TaskModel() { }

        public TaskModel(TaskVm taskVm)
        {
            this.Id = taskVm.Id;
            this.IsDone = taskVm.IsComplete;
            this.Member = taskVm.AssignedMemberId;
            this.Text = taskVm.Subject;
        }
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid Member { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; }

        public TaskVm ConvertToTaskVm()
        {
            return new TaskVm()
            {
                Id = this.Id,
                AssignedMemberId = this.Member,
                IsComplete = this.IsDone,
                Subject = this.Text
            };
        }
    }
}
