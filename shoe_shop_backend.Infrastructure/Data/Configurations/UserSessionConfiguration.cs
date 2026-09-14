using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shoe_shop_backend.Domain.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shoe_shop_backend.Infrastructure.Data.Configurations
{
    public class UserSessionConfiguration : IEntityTypeConfiguration<UserSession>
    {
        public void Configure(EntityTypeBuilder<UserSession> builder)
        {
            builder.ToTable("user_sessions");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id").HasMaxLength(26);
            builder.Property(x => x.UserId).HasColumnName("user_id").HasMaxLength(26).IsRequired();
            builder.Property(x => x.TokenHash).HasColumnName("token_hash").HasMaxLength(255).IsRequired();
            builder.Property(x => x.ExpiresAt).HasColumnName("expires_at").IsRequired();
            builder.Property(x => x.RevokedAt).HasColumnName("revoked_at");
            builder.Property(x => x.ReplacedBy).HasColumnName("replaced_by").HasMaxLength(26);
            builder.Property(x => x.IsActive).HasColumnName("is_active").HasDefaultValue(true);
            builder.Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(x => x.UpdateAt).HasColumnName("update_at")
                   .HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAddOrUpdate();
        }
    }
}
