using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Core.Data.Repositories;
using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Data.Infrastructure
{
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        #region Properties
        private DecanterContext dbContext = null;
        private readonly DbSet<T> dbSet;

        protected DecanterContext DbContext
        {
            get { return this.dbContext; }
            private set { }
        }

        #endregion

        public RepositoryBase(DecanterContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<T>();
        }

        public IQueryable<T> List
        {
            get
            {
                return this.dbSet;
            }
        }

        public virtual IEnumerable<T> GetAll()
        {
            return this.dbSet.ToList();
        }

        public virtual int Count()
        {
            return this.dbSet.Count();
        }

        public virtual IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = this.dbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.AsEnumerable();
        }

        public T GetSingle(int id)
        {
            return this.dbSet.FirstOrDefault(x => x.Equals(id));
        }

        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return this.dbSet.FirstOrDefault(predicate);
        }

        public T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = this.dbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.Where(predicate).FirstOrDefault();
        }

        public virtual IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return this.dbSet.Where(predicate);
        }

        public virtual void Add(T entity)
        {
            this.dbSet.Add(entity);
            this.dbContext.Entry(entity).State = EntityState.Added;
        }

        public virtual void Update(T entity)
        {
            this.dbSet.Update(entity);
            this.dbContext.Entry(entity).State = EntityState.Modified;
        }
        public virtual void Delete(T entity)
        {
            this.dbSet.Remove(entity);
            this.dbContext.Entry(entity).State = EntityState.Deleted;
        }

        public virtual void DeleteWhere(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> entities = dbSet.Where(predicate);

            foreach (var entity in entities)
            {
                this.dbContext.Entry<T>(entity).State = EntityState.Deleted;
            }
        }

        public virtual void Commit()
        {
            this.dbContext.Commit();
        }
    }
}
