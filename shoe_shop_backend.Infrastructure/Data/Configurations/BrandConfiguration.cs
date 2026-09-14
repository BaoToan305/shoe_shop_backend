using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shoe_shop_backend.Domain.Main;

namespace shoe_shop_backend.Infrastructure.Data.Configurations
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brands>
    {
        public void Configure(EntityTypeBuilder<Brands> builder)
        {
            builder.ToTable("brands");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id").HasMaxLength(26).IsFixedLength();
            builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
            builder.Property(x => x.LogoUrl).HasColumnName("logo_url").HasMaxLength(255);
            builder.Property(x => x.Description).HasColumnName("description");
            builder.Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(x => x.UpdatedAt).HasColumnName("update_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAddOrUpdate();
        }
    }
}
