using System;
using System.Collections.Generic;
using System.Text;
using ZoomPortalMonolith.SharedKernal.Domain.Foundation.Entities;

namespace ZoomPortalMonolith.SharedKernal.Infrastructure.Repositories
{
    public interface IWritableRepository<TEntity, in TId>
        : IRepository<TEntity, TId> where TEntity
        : IEntity<TId>, IAggregateRoot
    {

        void Add(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
    }
}
