using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZoomPortalMonolith.SharedKernal.Domain.Foundation.Entities;
using ZoomPortalMonolith.SharedKernal.Infrastructure;
using ZoomPortalMonolith.SharedKernal.Infrastructure.Repositories;

namespace ZoomPortalMonolith.Infrastructure.EntityFramework.Repositories
{
    public class BaseRepository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class, IEntity<TId>, IAggregateRoot
    {
        protected DbContext Context;
        protected DbSet<TEntity> DbSet => Context.Set<TEntity>();

        public BaseRepository(IDataContext context)
        {
            Context = (DbContext)context;
        }

        public virtual TEntity Get(TId id)
        {
            return DbSet.Local.FirstOrDefault(x => x.Id.Equals(id)) ?? DbSet.Find(id);
        }

        public virtual Task<TEntity> GetAsync(TId id)
        {
            var entity = DbSet.Local.FirstOrDefault(x => x.Id.Equals(id));
            return entity != null ? Task.FromResult(entity) : DbSet.FindAsync(id);
        }

        public virtual IEnumerable<TEntity> All()
        {
            return DbSet.AsNoTracking().ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> AllAsync()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> criteria)
        {
            return DbSet.Where(criteria).ToList();
        }
        public virtual async  Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> criteria)
        {
            return await DbSet.Where(criteria).ToListAsync();
        }

        public virtual bool Any(Expression<Func<TEntity, bool>> criteria)
        {
            return DbSet.Local.Any(criteria.Compile()) || DbSet.Any(criteria);
        }

        public virtual Task<bool> AnyAsync()
        {
            return DbSet.Local.Any() ? Task.FromResult(true) : DbSet.AnyAsync();
        }

        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> criteria)
        {
            return DbSet.Local.Any(criteria.Compile()) ? Task.FromResult(true) : DbSet.AnyAsync(criteria);
        }

        public void Dispose() => Context.Dispose();
    }
}
