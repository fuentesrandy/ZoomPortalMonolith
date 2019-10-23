using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZoomPortalMonolith.SharedKernal.Infrastructure;

namespace ZoomPortalMonolith.Infrastructure.EntityFramework.Context
{
    public abstract class DataContextBase<TContext> : DbContext, IDataContext where TContext : DbContext
    {
        private readonly IRequestContext _requestContext;

        public DataContextBase(DbContextOptions<TContext> options, IRequestContext requestContext)
            : base(options)
        {
            _requestContext = requestContext;

            

#if DEBUG
      
            Database.EnsureCreated();
#endif

#if DEBUG == false // only use in production 
          Database.EnsureCreated();
#endif
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


        public int Commit() => this.SaveChanges();
        public Task<int> CommitAsync() => this.SaveChangesAsync();
    }
}
