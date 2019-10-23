using System;
using ZoomPortalMonolith.SharedKernal.Domain.Foundation.Entities;

namespace ZoomPortalMonolith.Domain.CustomerManagement.Entities
{
    public class Customer : Entity<Guid>, IAggregateRoot
    {
        public string Name { get; private set; }


        protected internal  Customer()
        {
            
        }

        public Customer(string name) 
            : base(Guid.NewGuid())
        {
            Name = name;
        }

        public void Update(string name)
        {
            Name = name;
            ModifyDate = DateTime.UtcNow;
            
        }
    }
}
