using Backend.Domains.Player.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Domains.Player.Configuration;

public class PlayerEntityConfiguration :  IEntityTypeConfiguration<PlayerEntity>
{
    public void Configure(EntityTypeBuilder<PlayerEntity> builder)
    {
        builder.ToTable("Player");
        
        builder.HasKey(x => x.PlayerId);
        builder.Property(x => x.PlayerId).IsRequired();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(20);
        builder.Property(x => x.GroupName).IsRequired(); ;
    }
}