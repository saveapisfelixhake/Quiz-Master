using Backend.Domains.Bar.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Domains.Bar.Persistence.Sql.Configurations;

public class BarAddressEntityTypeConfiguration : IEntityTypeConfiguration<BarAddressEntity>
{
    public void Configure(EntityTypeBuilder<BarAddressEntity> builder)
    {
        builder.ToTable("BarAddresses");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.Street).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Number).HasMaxLength(20).IsRequired();

        builder.HasIndex(x => new { x.Street, x.Number }).IsUnique();
    }
}