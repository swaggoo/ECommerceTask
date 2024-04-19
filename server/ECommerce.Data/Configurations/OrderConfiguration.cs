using ECommerce.Core.Entities;
using ECommerce.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.CreatedOn).IsUnique();
        builder.Property(x => x.CustomerFullName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.CustomerPhone).IsRequired().HasMaxLength(100);
        builder.Property(x => x.OrderStatus)
            .HasDefaultValue(OrderStatus.Draft)
            .HasConversion(
                x => x.ToString(),
                dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));
    }
}