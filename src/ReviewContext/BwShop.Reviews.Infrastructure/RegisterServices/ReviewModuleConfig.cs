using Bw.Core.Web.Module;
using BwShop.Reviews.Infrastructure.RegisterServices.WebApplicationBuilderExtensions;
using BwShop.Reviews.Infrastructure.RegisterServices.WebApplicationExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace BwShop.Reviews.Infrastructure.RegisterServices;

public class ReviewModuleConfig : IModuleConfiguration
{
    public WebApplicationBuilder AddModuleServices(WebApplicationBuilder builder)
    {
        builder.RegisterInfrastructure();
        builder.AddServices();
        builder.AddStorage();
        return builder;
    }

    public async Task<WebApplication> ConfigureModule(WebApplication app)
    {
        app.UseInfrastructure();
        return app;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
         endpoints
            .MapGet(
                "/",
                (HttpContext context) =>
                {
                    var requestId = context.Request.Headers.TryGetValue(
                        "X-Request-InternalCommandId",
                        out var requestIdHeader
                    )
                        ? requestIdHeader.FirstOrDefault()
                        : string.Empty;

                    return $"Review Service Apis, RequestId: {requestId}";
                }
            )
            .ExcludeFromDescription();
        return endpoints;
    }
}