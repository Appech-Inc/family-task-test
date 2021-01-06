using AutoMapper;
using Core.Abstractions.Repositories;
using Core.Abstractions.Services;
using Domain.Commands;
using Domain.Queries;
using Domain.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task = Domain.DataModels.Task;

namespace Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public TaskService(IMapper mapper, ITaskRepository TaskRepository)
        {
            _mapper = mapper;
            _taskRepository = TaskRepository;
        }

        public async Task<CreateTaskCommandResult> CreateTaskCommandHandler(CreateTaskCommand command)
        {
            var task = _mapper.Map<Task>(command);
            var persistedTask = await _taskRepository.CreateRecordAsync(task);

            var vm = _mapper.Map<TaskVm>(persistedTask);

            return new CreateTaskCommandResult()
            {
                Payload = vm
            };
        }

        public async Task<AssignTaskCommandResult> AssignTaskCommandHandler(AssignTaskCommand command)
        {
            var isSucceed = true;
            var task = await _taskRepository.ByIdAsync(command.Id);

            _mapper.Map(command, task);

            var affectedRecordsCount = await _taskRepository.UpdateRecordAsync(task);
            if (affectedRecordsCount < 1)
                isSucceed = false;

            return new AssignTaskCommandResult
            {
                Succeed = isSucceed
            };
        }

        public async Task<CompleteTaskCommandResult> CompleteTaskCommandHandler(CompleteTaskCommand command)
        {
            var isSucceed = true;
            var task = await _taskRepository.ByIdAsync(command.Id);
            task.IsComplete = true;

            var affectedRecordsCount = await _taskRepository.UpdateRecordAsync(task);
            if (affectedRecordsCount < 1)
                isSucceed = false;

            return new CompleteTaskCommandResult
            {
                Succeed = isSucceed
            };
        }

        public async Task<GetAllTasksQueryResult> GetAllTasksQueryHandler()
        {
            var vm = new List<TaskVm>();
            var tasks = await _taskRepository.Reset().ToListAsync();

            if (tasks?.Any() ?? false)
                vm = _mapper.Map<List<TaskVm>>(tasks);

            return new GetAllTasksQueryResult
            {
                Payload = vm
            };
        }
    }
}