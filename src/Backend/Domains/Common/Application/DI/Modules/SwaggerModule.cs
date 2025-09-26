using Autofac;
using Autofac.Extensions.DependencyInjection;

namespace Backend.Domains.Common.Application.DI.Modules;

public class SwaggerModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var collection = new ServiceCollection();

        collection.AddSwaggerGen();

        builder.Populate(collection);
    }
}