using AutoMapper;
using Domain.Commands;
using Domain.DataModels;
using Domain.ViewModel;

namespace WebApi.AutoMapper
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<Task, TaskVm>();
            CreateMap<CreateTaskCommand, Task>();
            CreateMap<AssignTaskCommand, Task>();
            CreateMap<CompleteTaskCommand, Task>();
        }
    }
}
