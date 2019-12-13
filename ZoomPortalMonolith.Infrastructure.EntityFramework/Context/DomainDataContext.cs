using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ZoomPortalMonolith.Domain.CustomerManagement.Entities;
using ZoomPortalMonolith.Domain.ProjectManagement.Entities;
using ZoomPortalMonolith.Infrastructure.EntityFramework.Mapping.CustomerManagement;
using ZoomPortalMonolith.Infrastructure.EntityFramework.Mapping.ProjectManagement;
using ZoomPortalMonolith.Infrastructure.EntityFramework.Repositories;
using ZoomPortalMonolith.SharedKernal.Infrastructure;
using ZoomPortalMonolith.SharedKernal.Infrastructure.Repositories;

namespace ZoomPortalMonolith.Infrastructure.EntityFramework.Context
{
    public class DomainUnitOfWork : DataContextBase<DomainUnitOfWork>, IDomainUnitOfWork
    {
        public IWritableRepository<Customer, Guid> Customers { get; }
        public IWritableRepository<Project, Guid> Projects { get; }

        public DomainUnitOfWork(DbContextOptions<DomainUnitOfWork> options, IRequestContext requestContext)
            : base(options, requestContext)
        {
            Customers = new WritableBaseRepository<Customer, Guid>(this);
            Projects = new WritableBaseRepository<Project, Guid>(this);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.HasDefaultSchema("public");

            modelBuilder.ApplyConfiguration(new CustomerMap());
            modelBuilder.ApplyConfiguration(new ProjectMap());

        }


        
    }
}
