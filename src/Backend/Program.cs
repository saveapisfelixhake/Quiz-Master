using Autofac;
using Autofac.Extensions.DependencyInjection;
using Backend.Domains.Common.Application.DI.Modules;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>((context, containerBuilder) =>
        {
            containerBuilder.RegisterModule(new GraphQLModule(context.HostingEnvironment));
        }
    );

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthorization();

app.MapGraphQL();

await app.RunWithGraphQLCommandsAsync(args).ConfigureAwait(false);
