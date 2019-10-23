using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZoomPortalMonolith.SharedKernal.Domain.Foundation.Entities;

namespace ZoomPortalMonolith.SharedKernal.Infrastructure.Repositories
{
    public interface IRepository<TEntity, in TId> 
        : IDisposable where TEntity
        : IEntity<TId>, IAggregateRoot
    {

        TEntity Get(TId id);
        Task<TEntity> GetAsync(TId id);
        IEnumerable<TEntity> All();
        Task<IEnumerable<TEntity>> AllAsync();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> criteria);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> criteria);
        bool Any(Expression<Func<TEntity, bool>> criteria);
        Task<bool> AnyAsync();
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> criteria);
    }
}
