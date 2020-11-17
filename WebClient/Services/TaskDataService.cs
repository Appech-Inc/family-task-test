using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.Queries;
using Domain.ViewModel;
using Microsoft.AspNetCore.Components;
using WebClient.Abstractions;
using WebClient.Shared.Models;
using Domain.Commands;
using Core.Extensions.ModelConversion;

namespace WebClient.Services
{
    public class TaskDataService: ITaskDataService
    {
        private readonly HttpClient httpClient;
        private IEnumerable<TaskVm> taskVms;
        public TaskDataService(IHttpClientFactory clientFactory)
        {
            httpClient = clientFactory.CreateClient("FamilyTaskAPI");
            taskVms = new List<TaskVm>();
            LoadTasks();
        }

        private async void LoadTasks()
        {
            taskVms = (await GetAllTasks()).Payload;
            TasksUpdated?.Invoke(this, null);
        }

        private async Task<GetAllTasksQueryResult> GetAllTasks()
        {
            return await httpClient.GetJsonAsync<GetAllTasksQueryResult>("tasks");
        }

        private async Task<CreateTaskCommandResult> Create(CreateTaskCommand command)
        {            
            return await httpClient.PostJsonAsync<CreateTaskCommandResult>("tasks", command);
        }

        private async Task<UpdateTaskCommandResult> Update(UpdateTaskCommand command)
        {
            return await httpClient.PutJsonAsync<UpdateTaskCommandResult>($"tasks/{command.Id}", command);
        }

        public List<TaskModel> Tasks => taskVms.Select(t => new TaskModel(t)).ToList();
        public TaskModel SelectedTask { get; private set; }

        public event EventHandler TasksUpdated;
        public event EventHandler TaskSelected;
        public event EventHandler<string> CreateTaskFailed;
        public event EventHandler<string> UpdateTaskFailed;

        public void SelectTask(Guid id)
        {
            SelectedTask = Tasks.SingleOrDefault(t => t.Id == id);
            TasksUpdated?.Invoke(this, null);
        }

        public async Task ToggleTask(Guid id)
        {
            var taskModel = Tasks.FirstOrDefault(t => t.Id == id);
            taskModel.IsDone = !taskModel.IsDone;

            await UpdateTaskModel(taskModel);
        }

        public async Task AddTask(TaskModel model)
        {
            var taskVm = model.ConvertToTaskVm();
            var result = await Create(taskVm.ToCreateTaskCommand());

            if (result != null)
            {
                var updatedList = (await GetAllTasks()).Payload;

                if (updatedList != null)
                {
                    taskVms = updatedList;
                    TasksUpdated?.Invoke(this, null);
                    return;
                }
                UpdateTaskFailed?.Invoke(this, "The creation was successful, but we can no longer get an updated list of members from the server.");

                return;
            }

            CreateTaskFailed?.Invoke(this, "Unable to create record.");
        }

        public async Task ReassignTask(TaskModel taskModel, Guid newAssigneeId)
        {
            taskModel.Member = newAssigneeId;
            
            await UpdateTaskModel(taskModel);
        }

        private async Task UpdateTaskModel(TaskModel taskModel)
        {
            var taskVm = taskModel.ConvertToTaskVm();
            var result = await Update(taskVm.ToUpdateTaskCommand());

            if(result != null)
            {
                var updatedList = (await GetAllTasks()).Payload;

                if (updatedList != null)
                {
                    taskVms = updatedList;
                    TasksUpdated?.Invoke(this, null);
                    return;
                }
                UpdateTaskFailed?.Invoke(this, "The save was successful, but we can no longer get an updated list of members from the server.");

                return;
            }

            UpdateTaskFailed?.Invoke(this, "Unable to save changes.");
        }

    }
}