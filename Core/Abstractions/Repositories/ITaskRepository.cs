using Domain.DataModels;
using System;

namespace Core.Abstractions.Repositories
{
    public interface ITaskRepository : IBaseRepository<Guid, Task, ITaskRepository>
    {
    }
}