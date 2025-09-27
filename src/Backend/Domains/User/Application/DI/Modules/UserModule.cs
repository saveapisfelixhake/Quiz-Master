using Autofac;
using Autofac.Extensions.DependencyInjection;
using Backend.Domains.User.Domain.Options;

namespace Backend.Domains.User.Application.DI.Modules;

public class UserModule(IConfiguration configuration) : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var collection = new ServiceCollection();

        collection.Configure<AdminOption>(configuration.GetSection("Admin"));

        builder.Populate(collection);
    }
}
