using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZoomPortalMonolith.Domain.CustomerManagement.Entities;

namespace ZoomPortalMonolith.Infrastructure.EntityFramework.Mapping.CustomerManagement
{
    public class CustomerMap : EntityMap<Customer, Guid>
    {
        public override void Mappings(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer", "CustomerManagement");

            builder.Property(x => x.Name).IsRequired();
        }
    }
}
