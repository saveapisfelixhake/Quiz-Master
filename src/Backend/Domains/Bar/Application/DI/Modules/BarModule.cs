using Autofac;
using Autofac.Extensions.DependencyInjection;
using Backend.Domains.Bar.Application.Authentication.Handlers;
using Backend.Domains.Bar.Domain.Options.Authentication;
using Backend.Domains.Common.Infrastructure.Constants;

namespace Backend.Domains.Bar.Application.DI.Modules;

public class BarModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var collection = new ServiceCollection();

        collection.AddAuthentication()
            .AddScheme<BarBasicAuthenticationHandlerOption, BarBasicAuthenticationHandler>(AuthenticationSchemes.BarBasicAuth, "Bar Basic Authentication", null);

        builder.Populate(collection);
    }
}