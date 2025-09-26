using Backend.Domains.Bar.Domain.Models.Entities;
using Backend.Domains.Bar.Persistence.Sql.Configurations;
using Backend.Domains.Player.Configuration;
using Backend.Domains.Player.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Domains.Common.Persistence.Sql.Context;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<PlayerEntity> Players => Set<PlayerEntity>();
    public DbSet<BarEntity>  Bars => Set<BarEntity>();
    public DbSet<BarAddressEntity> BarAddresses => Set<BarAddressEntity>();
    public DbSet<PlayerGroupEntity> PlayerGroups => Set<PlayerGroupEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PlayerGroupEntityConfiguration());
        modelBuilder.ApplyConfiguration(new PlayerEntityConfiguration());
        modelBuilder.ApplyConfiguration(new BarEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new BarAddressEntityTypeConfiguration());
    }
}