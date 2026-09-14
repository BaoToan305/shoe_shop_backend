using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shoe_shop_backend.Domain.Main;


namespace shoe_shop_backend.Infrastructure.Data
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("product");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id").HasMaxLength(26).IsFixedLength();
            builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
            builder.Property(x => x.BrandId).HasColumnName("brand_id").HasMaxLength(26).IsFixedLength().IsRequired();
            builder.Property(x => x.CategoryId).HasColumnName("category_id").HasMaxLength(26).IsFixedLength().IsRequired();
            builder.Property(x => x.Slug).HasColumnName("slug").HasMaxLength(220);
            builder.Property(x => x.Description).HasColumnName("description");
            builder.Property(x => x.Price).HasColumnName("price").HasColumnType("decimal(12,2)");
            builder.Property(x => x.Gender).HasColumnName("gender");
            builder.Property(x => x.IsActive).HasColumnName("is_active");
            builder.Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP"); ;
            builder.Property(x => x.UpdatedAt).HasColumnName("update_at").HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAddOrUpdate(); ;
        }
    }
}
