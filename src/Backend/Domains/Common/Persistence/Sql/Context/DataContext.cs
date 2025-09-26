using Backend.Domains.Bar.Domain.Models.Entities;
using Backend.Domains.Bar.Persistence.Sql.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Backend.Domains.Common.Persistence.Sql.Context;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<BarEntity>  Bars => Set<BarEntity>();
    public DbSet<BarAddressEntity> BarAddresses => Set<BarAddressEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BarEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new BarAddressEntityTypeConfiguration());
    }
}