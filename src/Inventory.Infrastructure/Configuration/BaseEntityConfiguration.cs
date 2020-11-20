using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Infrastructure.Configuration
{
    /// <summary>
    /// Represents the base configuration of all entities
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity</typeparam>
    abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Configures the base properties of all entities
        /// </summary>
        /// <param name="builder"></param>
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(x => x.CreationBy)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(x => x.CreationDate)
                .IsRequired(false);

            builder.Property(x => x.ModificationBy)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(x => x.ModificationDate)
                .IsRequired(false);

            ConfigureEntity(builder);
        }

        /// <summary>
        /// Configures the entity DB properties
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type</param>
        protected abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);
    }
}