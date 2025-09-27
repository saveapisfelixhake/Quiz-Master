using Backend.Domains.Player.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Domains.Player.Configuration;

public class PlayerGroupEntityConfiguration : IEntityTypeConfiguration<PlayerGroupEntity>
{
    public void Configure(EntityTypeBuilder<PlayerGroupEntity> builder)
    {
        builder.ToTable("PlayerGroup");
        
        builder.HasKey(x => x.GroupId);
        builder.Property(x => x.GroupId).IsRequired();
        builder.Property(x => x.GroupName).IsRequired().HasMaxLength(30);
        builder.Property(x => x.BarName).IsRequired(); ;
    }
}