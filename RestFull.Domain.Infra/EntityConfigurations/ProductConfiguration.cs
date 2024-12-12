using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestFull.Domain.Core.Entities;

using static RestFull.Domain.Infra.EntityConfigurations.ProductSettings;

namespace RestFull.Domain.Infra.EntityConfigurations;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{

    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name).IsRequired().HasMaxLength(NameMaxLength);
        builder.Property(a => a.Description).IsRequired().HasMaxLength(DescriptionMaxLength);
        builder.Property(a => a.Price).HasPrecision(10, 2);
        builder.Property(a => a.Billing).HasPrecision(10, 2);

        builder.HasIndex(a => a.Name, "IX_PRODUCT_NAME").IsDescending();
    }
}

public static class ProductSettings
{
    public const int NameMaxLength = 50;
    public const int DescriptionMaxLength = 255;
}
