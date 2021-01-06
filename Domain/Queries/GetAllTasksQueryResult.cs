using Domain.ViewModel;
using System.Collections.Generic;

namespace Domain.Queries
{
    public class GetAllTasksQueryResult
    {
        public IEnumerable<TaskVm> Payload { get; set; }
    }
}
