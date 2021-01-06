using Core.Extensions.ModelConversion;
using Domain.Commands;
using Domain.Queries;
using Domain.ViewModel;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebClient.Abstractions;

namespace WebClient.Services
{
    public class TaskDataService: ITaskDataService
    {
        private readonly HttpClient _httpClient;
        private IEnumerable<TaskVm> _tasks;

        public TaskDataService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("FamilyTaskAPI");
            LoadTasks();
        }

        public IEnumerable<TaskVm> Tasks => _tasks;
        public TaskVm SelectedTask { get; private set; }
        public TaskVm DragedTask { get; private set; }

        public event EventHandler TasksUpdated;
        public event EventHandler TaskSelected;
        public event EventHandler<string> AddTaskFailed;
        public event EventHandler<string> CompleteTaskFailed;

        public void SelectTask(Guid id)
        {
            SelectedTask = _tasks.SingleOrDefault(t => t.Id == id);
            TasksUpdated?.Invoke(this, null);
        }

        public void SelectDraggedTask(Guid id)
        {
            DragedTask = _tasks.SingleOrDefault(t => t.Id == id);
        }

        public async Task ToggleTask(Guid id)
        {
            var taskVm = _tasks.SingleOrDefault(task => task.Id == id);
            var result = await Complete(taskVm.ToCompleteTaskCommand());
            if (result?.Succeed ?? false)
            {
                taskVm.IsComplete = true;
                TasksUpdated?.Invoke(this, null);
            }
            else
            {
                CompleteTaskFailed.Invoke(this, "Unable to complete task.");
            }
        }

        public async Task AddTask(TaskVm model)
        {
            var result = await Create(model.ToCreateTaskCommand());
            if (result != null)
            {
                var updatedList = (await GetAllTasks()).Payload;
                if (updatedList != null)
                {
                    _tasks = updatedList;
                    TasksUpdated?.Invoke(this, null);
                    return;
                }
                AddTaskFailed?.Invoke(this, "The creation was successful, but we can no longer get an updated list of tasks from the server.");
            }
            else
            {
                AddTaskFailed?.Invoke(this, "Unable to create record.");
            }
        }

        public async Task AssignTask(TaskVm model)
        {
            var result = await Assign(model.ToAssignTaskCommand());
            if (result?.Succeed ?? false)
            {
                var updatedList = (await GetAllTasks()).Payload;
                if (updatedList != null)
                {
                    _tasks = updatedList;
                    TasksUpdated?.Invoke(this, null);
                    return;
                }
                AddTaskFailed?.Invoke(this, "The assignment was successful, but we can no longer get an updated list of tasks from the server.");
            }
            else
            {
                AddTaskFailed?.Invoke(this, "Unable to assign the task.");
            }
        }

        private async void LoadTasks()
        {
            _tasks = (await GetAllTasks()).Payload;
            TasksUpdated?.Invoke(this, null);
        }

        private async Task<CreateTaskCommandResult> Create(CreateTaskCommand command) =>
            await _httpClient.PostJsonAsync<CreateTaskCommandResult>("tasks", command);

        private async Task<AssignTaskCommandResult> Assign(AssignTaskCommand command) =>
            await _httpClient.PutJsonAsync<AssignTaskCommandResult>("tasks/assign", command);

        private async Task<CompleteTaskCommandResult> Complete(CompleteTaskCommand command) =>
            await _httpClient.PutJsonAsync<CompleteTaskCommandResult>("tasks/complete", command);

        private async Task<GetAllTasksQueryResult> GetAllTasks() =>
            await _httpClient.GetJsonAsync<GetAllTasksQueryResult>("tasks");
    }
}