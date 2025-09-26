using Microsoft.EntityFrameworkCore;

namespace Backend.Domains.Common.Persistence.Sql.Context;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //TODO: Add entity configurations here
        //modelBuilder.ApplyConfiguration(...):
    }
}