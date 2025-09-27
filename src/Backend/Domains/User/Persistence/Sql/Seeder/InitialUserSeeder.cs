using Backend.Domains.Common.Infrastructure.Persistence.Sql.Seeder;
using Backend.Domains.Common.Infrastructure.Services;
using Backend.Domains.Quiz.Persistence.Sql.Context;
using Backend.Domains.User.Domain.Models.Entities;
using Backend.Domains.User.Domain.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Backend.Domains.User.Persistence.Sql.Seeder;

public class InitialUserSeeder(IHashingService hashingService, IOptionsMonitor<AdminOption> monitor) : BaseSeeder
{
    private AdminOption Option => monitor.CurrentValue;

    public override async Task<bool> CheckConditionAsync(DataContext context)
    {
        return !await context.Users.AnyAsync(x => x.IsInitialUser).ConfigureAwait(false);
    }

    public override async Task SeedAsync(DataContext context)
    {
        var passwordHash = hashingService.Hash(Option.Password);
        var initialUser = UserEntity.Create(Option.FirstName, Option.LastName, Option.Email, Option.UserName, passwordHash, true, true);

        await context.Users.AddAsync(initialUser).ConfigureAwait(false);
    }
}
