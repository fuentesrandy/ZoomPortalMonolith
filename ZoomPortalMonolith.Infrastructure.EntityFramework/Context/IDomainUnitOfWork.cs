
using System;
using ZoomPortalMonolith.Domain.CustomerManagement.Entities;
using ZoomPortalMonolith.Domain.ProjectManagement.Entities;
using ZoomPortalMonolith.SharedKernal.Infrastructure;
using ZoomPortalMonolith.SharedKernal.Infrastructure.Repositories;

namespace ZoomPortalMonolith.Infrastructure.EntityFramework.Context
{
    public interface IDomainUnitOfWork : IDataContext
    {

        IWritableRepository<Customer, Guid> Customers { get; }
        IWritableRepository<Project, Guid> Projects { get; }
    }
}
