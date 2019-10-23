using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ZoomPortalMonolith.SharedKernal.Domain.Foundation.Entities;

namespace ZoomPortalMonolith.Infrastructure.EntityFramework.Mapping
{
    public interface IEntityMap<TEntity> where TEntity : class
    {
        void Mappings(EntityTypeBuilder<TEntity> builder);
    }

    public class EntityMap<TEntity, TId>
        : IEntityMap<TEntity>, IEntityTypeConfiguration<TEntity>
        where TEntity : class, IEntity<TId>

    {
        public void DefaultMappings(EntityTypeBuilder<TEntity> builder)
        {
            var idType = typeof(TId);
            if (idType == typeof(Guid))
            {
                var guidConverter = new ValueConverter<Guid, string>(
                    v => v.ToString(),
                    v => new Guid(v));

                builder.Property(x => x.Id).HasConversion(guidConverter);
            }



            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreateDate).IsRequired().HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.ModifyDate).IsRequired();
        }

        public virtual void Mappings(EntityTypeBuilder<TEntity> builder)
        {
            throw new NotImplementedException();
        }

        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            DefaultMappings(builder);
            Mappings(builder);

        }
    }
}
