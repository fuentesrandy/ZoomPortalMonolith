using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZoomPortalMonolith.SharedKernal.Domain.Foundation.Entities;
using ZoomPortalMonolith.SharedKernal.Infrastructure;
using ZoomPortalMonolith.SharedKernal.Infrastructure.Repositories;

namespace ZoomPortalMonolith.Infrastructure.EntityFramework.Repositories
{
    public class WritableBaseRepository<TEntity, TId>
        : BaseRepository<TEntity, TId>, IWritableRepository<TEntity, TId> where TEntity
        : class, IEntity<TId>, IAggregateRoot
    {

        public WritableBaseRepository(IDataContext context) : base(context)
        {
            this.Context = (DbContext)context;
        }

        public virtual void Add(TEntity entity)
        {
            this.DbSet.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            var entry = Context.Entry(entity);
            this.DbSet.Attach(entity);
            entry.State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entity)
        {
            this.DbSet.Remove(entity);
        }


    }
}
