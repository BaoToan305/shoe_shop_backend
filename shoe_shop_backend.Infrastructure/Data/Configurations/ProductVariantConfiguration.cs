using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shoe_shop_backend.Domain.Main;

namespace shoe_shop_backend.Infrastructure.Data.Configurations
{
    public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
    {
        public void Configure(EntityTypeBuilder<ProductVariant> builder)
        {
            builder.ToTable("product_variants");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id").HasMaxLength(26).IsFixedLength();
            builder.Property(x => x.ProductId).HasColumnName("product_id").HasMaxLength(26).IsFixedLength().IsRequired();
            builder.Property(x => x.Sku).HasColumnName("sku").HasMaxLength(64).IsRequired();
            builder.HasIndex(x => x.Sku).IsUnique();
            builder.Property(x => x.Size).HasColumnName("size").HasMaxLength(10).IsRequired();
            builder.Property(x => x.Color).HasColumnName("color").HasMaxLength(50).IsRequired();
            builder.Property(x => x.PriceModifier).HasColumnName("price_modifier").HasColumnType("decimal(12,2)");
            builder.Property(x => x.StockQuantity).HasColumnName("stock_quantity");
            builder.Property(x => x.IsActive).HasColumnName("is_active");
            builder.Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP"); ;
            builder.Property(x => x.UpdatedAt).HasColumnName("update_at").HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAddOrUpdate(); ;
        }
    }
}
