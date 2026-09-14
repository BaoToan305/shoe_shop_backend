using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shoe_shop_backend.Domain.Main;

namespace shoe_shop_backend.Infrastructure.Data.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("cart_items");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id").HasMaxLength(26).IsFixedLength();
            builder.Property(x => x.CartId).HasColumnName("cart_id").HasMaxLength(26).IsFixedLength().IsRequired();
            builder.Property(x => x.ProductVariantId).HasColumnName("product_variant_id").HasMaxLength(26).IsFixedLength().IsRequired();
            builder.Property(x => x.Quantity).HasColumnName("quantity");
            builder.Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP"); ;
            builder.Property(x => x.UpdatedAt).HasColumnName("update_at").HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAddOrUpdate(); ;
        }
    }
}
