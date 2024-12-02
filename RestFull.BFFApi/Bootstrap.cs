using Microsoft.OpenApi.Models;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;

internal static class Bootstrap
{
    internal static void UseSwaggerMiddlare(this WebApplication app)
    {
        app.UseSwaggerForOcelotUI(opt =>
        {
            opt.PathToSwaggerGenerator = "/swagger/docs";
        });
        app.UseSwagger();
    }

    internal static void AddSwaggerService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerForOcelot(configuration, options =>
        {
            options.GenerateDocsDocsForGatewayItSelf(opt =>
            {
                opt.GatewayDocsOpenApiInfo = new OpenApiInfo()
                {
                    Title = "Gateway",
                    Version = "v1",
                };
            });
        });
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Gateway API",
                Version = "v1",
            });
        });
    }

    internal static void AddOcelotService(this IServiceCollection services, ConfigurationManager configuration)
    {
        configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
        services.AddOcelot(configuration)
            .AddCacheManager(x => x.WithDictionaryHandle());
    }
}