using Core.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private DecanterContext dbContext;
        private Dictionary<Type, object> repositoryList;

        public UnitOfWork(DecanterContext context)
        {
            this.dbContext = context;
        }

        public DecanterContext DbContext
        {
            get { return dbContext; }
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (this.repositoryList == null)
            {
                this.repositoryList = new Dictionary<Type, object>();
            }

            object repository;

            if (this.repositoryList.TryGetValue(typeof(TEntity), out repository) == false)
            {
                repository = new RepositoryBase<TEntity>(this.dbContext);
                this.repositoryList[typeof(TEntity)] = repository;
            }
            return (IRepository<TEntity>)repository;
        }

        public void Commit()
        {
            DbContext.Commit();
        }

        public void Dispose()
        {
            this.DbContext.Dispose();
        }
    }
}
