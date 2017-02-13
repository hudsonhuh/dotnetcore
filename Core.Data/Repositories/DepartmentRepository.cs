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
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(DecanterContext context)
            : base(context)
        {
        }

        public override void Update(Department entity)
        {
            entity.RegDate = DateTime.Now;
            base.Update(entity);
        }
    }

    public interface IDepartmentRepository : IRepository<Department>
    {
    }
}
