using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZoomPortalMonolith.Domain.ProjectManagement.Entities;

namespace ZoomPortalMonolith.Infrastructure.EntityFramework.Mapping.ProjectManagement
{
    public class ProjectMap : EntityMap<Project, Guid>
    {
        public override void Mappings(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Project", "ProjectManagement");
            builder.Property(x => x.Title).IsRequired();

            builder.HasOne(x => x.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .IsRequired();

        }
    }
}
