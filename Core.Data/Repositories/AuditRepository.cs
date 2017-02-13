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
    public class AuditRepository : RepositoryBase<Audit>, IAuditRepository
    {
        public AuditRepository(DecanterContext context)
            : base(context)
        {
        }
    }

    public interface IAuditRepository : IRepository<Audit>
    {
    }
}
