using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shoe_shop_backend.Domain.Main;
namespace shoe_shop_backend.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.ToTable("users");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id").HasMaxLength(26).IsFixedLength();
            builder.Property(x => x.FullName).HasColumnName("full_name").HasMaxLength(100).IsRequired();
            builder.Property(x => x.UserName).HasColumnName("user_name").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Password).HasColumnName("password").HasMaxLength(255).IsRequired();
            builder.Property(x => x.Email).HasColumnName("email").HasMaxLength(150);
            builder.Property(x => x.Phone).HasColumnName("phone").HasMaxLength(20);
            builder.Property(x => x.RoleId).HasColumnName("role_id").HasMaxLength(26).IsFixedLength().IsRequired();
            builder.Property(x => x.IsActive).HasColumnName("is_active");
            builder.Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP"); ;
            builder.Property(x => x.UpdatedAt).HasColumnName("update_at").HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAddOrUpdate(); ;
        }
    }
}
