using Autofac;
using Autofac.Extensions.DependencyInjection;
using Backend.Domains.Common.Application.DI.Modules;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>((_, containerBuilder) =>
        {
            containerBuilder.RegisterModule<CoreModule>();

            containerBuilder.RegisterModule<RestModule>();
            containerBuilder.RegisterModule<SwaggerModule>();
        }
    );

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

await app.StartAsync().ConfigureAwait(false);
await app.WaitForShutdownAsync().ConfigureAwait(false);

