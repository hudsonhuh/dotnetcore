using Core.Data.Infrastructure;
using Core.Entity.Decanter;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Repositories
{
    public class MemberRoleRepository : RepositoryBase<MemberRole>, IMemberRoleRepository
    {
        public MemberRoleRepository(DecanterContext context)
            : base(context)
        {
        }

        public override void Update(MemberRole entity)
        {
            entity.RegDate = DateTime.Now;
            base.Update(entity);
        }
    }

    public interface IMemberRoleRepository : IRepository<MemberRole>
    {
    }
}
