using Core.Abstractions.Repositories;
using Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class MemberRepository : BaseRepository<Guid, Member, MemberRepository>, IMemberRepository
    {
        public MemberRepository(FamilyTaskContext context) : base(context)
        { }

        IMemberRepository IBaseRepository<Guid, Member, IMemberRepository>.CommitTransaction()
        {
            return base.CommitTransaction();
        }

        IMemberRepository IBaseRepository<Guid, Member, IMemberRepository>.NoTrack()
        {
            return base.NoTrack();
        }

        IMemberRepository IBaseRepository<Guid, Member, IMemberRepository>.Reset()
        {
            return base.Reset();
        }

        IMemberRepository IBaseRepository<Guid, Member, IMemberRepository>.RollbackTransaction()
        {
            return base.RollbackTransaction();
        }

        IMemberRepository IBaseRepository<Guid, Member, IMemberRepository>.StartTransaction()
        {
            return base.StartTransaction();
        }
    }
}
