using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Configuration
{
    /// <summary>
    /// Entity framework configuration of the product
    /// </summary>
    class ProductConfiguration : BaseEntityConfiguration<Product>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();
            builder.HasIndex(x => x.Name)
                .IsUnique();

            builder.Property(x => x.DepositDate)
                .IsRequired();

            builder.Property(x => x.ExpirationDate)
                .IsRequired(false);

            builder.Property(x => x.ProductType)
                .HasMaxLength(50)
                .IsRequired(false);
        }
    }
}
