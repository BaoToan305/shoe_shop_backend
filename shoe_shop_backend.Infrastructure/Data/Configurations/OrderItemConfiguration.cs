using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shoe_shop_backend.Domain.Main;

namespace shoe_shop_backend.Infrastructure.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("order_items");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id").HasMaxLength(26).IsFixedLength();
            builder.Property(x => x.OrderId).HasColumnName("order_id").HasMaxLength(26).IsFixedLength().IsRequired();
            builder.Property(x => x.ProductVariantId).HasColumnName("product_variant_id").HasMaxLength(26).IsFixedLength().IsRequired();
            builder.Property(x => x.ProductName).HasColumnName("product_name").HasMaxLength(200).IsRequired();
            builder.Property(x => x.Size).HasColumnName("size").HasMaxLength(10).IsRequired();
            builder.Property(x => x.Color).HasColumnName("color").HasMaxLength(50).IsRequired();
            builder.Property(x => x.UnitPrice).HasColumnName("unit_price").HasColumnType("decimal(12,2)");
            builder.Property(x => x.Quantity).HasColumnName("quantity");
            builder.Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP"); ;
            builder.Property(x => x.UpdatedAt).HasColumnName("update_at").HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAddOrUpdate(); ;
        }
    }
}
