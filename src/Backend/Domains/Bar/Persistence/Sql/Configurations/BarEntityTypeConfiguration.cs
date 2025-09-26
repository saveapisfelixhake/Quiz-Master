using Backend.Domains.Bar.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Domains.Bar.Persistence.Sql.Configurations;

public class BarEntityTypeConfiguration : IEntityTypeConfiguration<BarEntity>
{
    public void Configure(EntityTypeBuilder<BarEntity> builder)
    {
        builder.ToTable("Bars");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.AddressId).IsRequired();

        builder.HasIndex(x => x.Name).IsUnique();
        builder.HasIndex(x => x.AddressId).IsUnique();

        builder.HasOne(x => x.Address).WithOne().HasForeignKey<BarEntity>(x => x.AddressId);
    }
}