using Bw.Core.Web.Module;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace BwShop.Catalog.Infrastructure.RegisterServices;

public class CatalogModuleconfiguration : IModuleConfiguration
{
    public WebApplicationBuilder AddModuleServices(WebApplicationBuilder builder)
    {
        throw new NotImplementedException();
    }

    public Task<WebApplication> ConfigureModule(WebApplication app)
    {
        throw new NotImplementedException();
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        throw new NotImplementedException();
    }
}