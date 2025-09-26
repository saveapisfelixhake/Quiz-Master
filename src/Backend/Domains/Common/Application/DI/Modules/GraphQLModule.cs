using Autofac;
using Autofac.Extensions.DependencyInjection;

namespace Backend.Domains.Common.Application.DI.Modules;

public class GraphQLModule(IHostEnvironment environment) : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var collection = new ServiceCollection();

        collection.AddGraphQLServer()
            .AddTypes()
            .AddSorting()
            .AddFiltering()
            .AddProjections()
            .ModifyPagingOptions(options =>
            {
                options.DefaultPageSize = 100;
                options.MaxPageSize = 1_000;
                options.IncludeTotalCount = true;
            })
            .ModifyRequestOptions(options => options.IncludeExceptionDetails = environment.IsDevelopment());
        
        builder.Populate(collection);
    }
}