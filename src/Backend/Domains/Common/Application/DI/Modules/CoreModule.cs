using Autofac;

namespace Backend.Domains.Common.Application.DI.Modules;

public class CoreModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(ThisAssembly)
            .AsImplementedInterfaces();
    }
}