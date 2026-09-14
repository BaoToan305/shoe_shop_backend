using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shoe_shop_backend.Domain.Main;

namespace shoe_shop_backend.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("orders");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id").HasMaxLength(26).IsFixedLength();
            builder.Property(x => x.UserId).HasColumnName("user_id").HasMaxLength(26).IsFixedLength().IsRequired();
            builder.Property(x => x.AddressId).HasColumnName("address_id").HasMaxLength(26).IsFixedLength().IsRequired();
            builder.Property(x => x.OrderCode).HasColumnName("order_code").HasMaxLength(30).IsRequired();
            builder.HasIndex(x => x.OrderCode).IsUnique();
            builder.Property(x => x.Status).HasColumnName("status");
            builder.Property(x => x.PaymentMethod).HasColumnName("payment_method");
            builder.Property(x => x.PaymentStatus).HasColumnName("payment_status");
            builder.Property(x => x.Subamount).HasColumnName("subamount").HasColumnType("decimal(12,2)");
            builder.Property(x => x.DiscountAmount).HasColumnName("discount_amount").HasColumnType("decimal(12,2)");
            builder.Property(x => x.TotalAmount).HasColumnName("total_amount").HasColumnType("decimal(12,2)");
            builder.Property(x => x.Notes).HasColumnName("notes").HasMaxLength(255);
            builder.Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP"); ;
            builder.Property(x => x.UpdatedAt).HasColumnName("update_at").HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAddOrUpdate(); ;
        }
    }
}
