using Autofac;
using Autofac.Extensions.DependencyInjection;
using Backend.Domains.Bar.Application.DI.Modules;
using Backend.Domains.Common.Application.DI.Modules;
using Backend.Domains.Common.Domain.Options;
using Backend.Domains.Common.Infrastructure.Persistence.Sql.Seeder;
using Backend.Domains.Common.Persistence.Sql.Context;
using Backend.Domains.User.Application.DI.Modules;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

if (EF.IsDesignTime)
{
    const string connectionString = "Server=localhost;";
    builder.Services.AddDbContext<DataContext>(options =>
        options.UseMySql(connectionString, MySqlServerVersion.LatestSupportedServerVersion));
}
else
{
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
        .ConfigureContainer<ContainerBuilder>((context, containerBuilder) =>
            {
                containerBuilder.RegisterModule<CoreModule>();

                containerBuilder.RegisterModule<RestModule>();
                containerBuilder.RegisterModule<SwaggerModule>();

                containerBuilder.RegisterModule(new UserModule(context.Configuration));
                containerBuilder.RegisterModule<BarModule>();
            }
        );

    builder.Services.Configure<MySqlOption>(builder.Configuration.GetSection("MySql"));
    builder.Services.AddPooledDbContextFactory<DataContext>((provider, optionsBuilder) =>
    {
        var monitor = provider.GetRequiredService<IOptionsMonitor<MySqlOption>>();
        var option = monitor.CurrentValue;

        optionsBuilder.UseMySql(option.ToString(), ServerVersion.AutoDetect(option.ToString()));
    });
}

builder.Services.AddAuthorization();

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<DataContext>>();
    await using var context = await factory.CreateDbContextAsync().ConfigureAwait(false);

    await context.Database.MigrateAsync().ConfigureAwait(false);

    var seeders = scope.ServiceProvider.GetRequiredService<IEnumerable<ISeeder>>();
    foreach (var seeder in seeders)
    {
        if (await seeder.CheckConditionAsync(context).ConfigureAwait(false))
        {
            await seeder.SeedAsync(context).ConfigureAwait(false);
        }
    }

    await context.SaveChangesAsync().ConfigureAwait(false);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.StartAsync().ConfigureAwait(false);
await app.WaitForShutdownAsync().ConfigureAwait(false);