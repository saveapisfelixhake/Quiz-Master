using Backend.Domains.User.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Domains.User.Persistence.Sql.Configurations;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id).IsRequired().ValueGeneratedNever();
        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Email).IsRequired(false).HasMaxLength(100);

        builder.Property(x => x.UserName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.PasswordHash).IsRequired(false).HasMaxLength(60);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.IsInitialUser).IsRequired().HasDefaultValue(false);

        builder.HasIndex(x => x.UserName).IsUnique();
        builder.HasIndex(x => x.IsActive);
    }
}