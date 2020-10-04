using System;
using System.Collections.Generic;
using System.Linq;
using WebClient.Abstractions;
using WebClient.Shared.Models;

namespace WebClient.Services
{
    public class TaskDataService: ITaskDataService
    {
        public TaskDataService()
        {
            Tasks = new List<TaskModel>();
        }




        public List<TaskModel> Tasks { get; private set; }
        public TaskModel SelectedTask { get; private set; }


        public event EventHandler TasksUpdated;
        public event EventHandler TaskSelected;

        public void SelectTask(Guid id)
        {
            SelectedTask = Tasks.SingleOrDefault(t => t.Id == id);
            TasksUpdated?.Invoke(this, null);
        }

        public void ToggleTask(Guid id)
        {
            foreach (var taskModel in Tasks)
            {
                if (taskModel.Id == id)
                {
                    taskModel.IsDone = !taskModel.IsDone;
                }
            }

            TasksUpdated?.Invoke(this, null);
        }

        public void AddTask(TaskModel model)
        {
            Tasks.Add(model);
            TasksUpdated?.Invoke(this, null);
        }
    }
}