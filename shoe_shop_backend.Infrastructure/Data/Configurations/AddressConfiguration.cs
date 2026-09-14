using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shoe_shop_backend.Domain.Main;

namespace shoe_shop_backend.Infrastructure.Data.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("addresses");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id").HasMaxLength(26).IsFixedLength();
            builder.Property(x => x.UserId).HasColumnName("user_id").HasMaxLength(26).IsFixedLength().IsRequired();
            builder.Property(x => x.RecipientName).HasColumnName("recipient_name").HasMaxLength(150).IsRequired();
            builder.Property(x => x.Phone).HasColumnName("phone").HasMaxLength(20).IsRequired();
            builder.Property(x => x.Province).HasColumnName("province").HasMaxLength(100).IsRequired();
            builder.Property(x => x.District).HasColumnName("district").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Ward).HasColumnName("ward").HasMaxLength(100);
            builder.Property(x => x.DetailAddress).HasColumnName("detail_address").HasMaxLength(255).IsRequired();
            builder.Property(x => x.IsDefault).HasColumnName("is_default");
            builder.Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP"); ;
            builder.Property(x => x.UpdatedAt).HasColumnName("update_at").HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAddOrUpdate(); ;
        }
    }
}
