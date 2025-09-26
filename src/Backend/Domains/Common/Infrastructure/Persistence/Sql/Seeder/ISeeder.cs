using Backend.Domains.Quiz.Persistence.Sql.Context;

namespace Backend.Domains.Common.Infrastructure.Persistence.Sql.Seeder;

public interface ISeeder
{
    Task<bool> CheckConditionAsync(DataContext context);
    Task SeedAsync(DataContext context);
}
