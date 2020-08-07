using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Commands;
using Domain.Queries;

namespace WebClient.Abstractions
{
    public interface IMemberDataService
    {
        public Task<CreateMemberCommandResult> Create(CreateMemberCommand command);
        public Task<UpdateMemberCommandResult> Update(UpdateMemberCommand command);
        public Task<GetAllMembersQueryResult> GetAllMembers();
    }
}
