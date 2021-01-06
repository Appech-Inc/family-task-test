using Domain.Commands;
using Domain.Queries;
using System.Threading.Tasks;

namespace Core.Abstractions.Services
{
    public interface ITaskService
    {
        Task<CreateTaskCommandResult> CreateTaskCommandHandler(CreateTaskCommand command);
        Task<AssignTaskCommandResult> AssignTaskCommandHandler(AssignTaskCommand command);
        Task<CompleteTaskCommandResult> CompleteTaskCommandHandler(CompleteTaskCommand command);
        Task<GetAllTasksQueryResult> GetAllTasksQueryHandler();
    }
}