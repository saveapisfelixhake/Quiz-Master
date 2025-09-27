using Backend.Domains.Quiz.Persistence.Sql.Context;

namespace Backend.Domains.Common.Infrastructure.Persistence.Sql.Seeder;

public abstract class BaseSeeder : ISeeder
{
    public virtual Task<bool> CheckConditionAsync(DataContext context)
    {
        return Task.FromResult(CheckCondition(context));
    }
    protected virtual bool CheckCondition(DataContext context)
    {
        return true;
    }

    public abstract Task SeedAsync(DataContext context);
}
