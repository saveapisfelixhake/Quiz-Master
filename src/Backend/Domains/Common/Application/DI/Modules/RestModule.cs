using Autofac;
using Autofac.Extensions.DependencyInjection;

namespace Backend.Domains.Common.Application.DI.Modules;

public class RestModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var collection = new ServiceCollection();

        collection.AddControllers().AddApplicationPart(ThisAssembly).AddNewtonsoftJson();
        
        builder.Populate(collection);
    }
}