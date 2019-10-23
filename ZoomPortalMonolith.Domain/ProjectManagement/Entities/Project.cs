
using System;
using ZoomPortalMonolith.Domain.CustomerManagement.Entities;
using ZoomPortalMonolith.SharedKernal.Domain.Foundation.Entities;

namespace ZoomPortalMonolith.Domain.ProjectManagement.Entities
{
    public class Project : Entity<Guid>, IAggregateRoot
    {

        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; private set; }
       

        public string Title { get; private set; }

        protected internal Project()
        {
            
        }


        public Project(string title, Customer customer)
            : base(Guid.NewGuid())
        {
            Title = title;
            Customer = customer;
        }

        public void Update(string name)
        {
            Title = name;
            ModifyDate = DateTime.UtcNow;

        }
    }
}
