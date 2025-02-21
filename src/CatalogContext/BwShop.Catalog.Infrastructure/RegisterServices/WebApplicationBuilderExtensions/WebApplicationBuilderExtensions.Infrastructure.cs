using Bw.Caching.Behaviours;
using Bw.Extensions;
using Bw.Extensions.Cqrs;
using Bw.Logging;
using Bw.Logging.Extensions;
using Bw.Persistence.EFCore;
using Bw.Swagger;
using Bw.Validation;
using Bw.Web.Extensions;
using BwShop.Catalog.Api;
using BwShop.Catalog.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BwShop.Catalog.Infrastructure.RegisterServices.WebApplicationBuilderExtensions;

public static partial class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder Registerinfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddCore(builder.Configuration);

        builder.AddCompression();
        // builder.AddCustomProblemDetails();

        builder.AddCustomSerilog();


        DotNetEnv.Env.TraversePath().Load();

        builder.AddCustomVersioning();
        builder.AddCustomSwagger(typeof(CatalogApiAssemblyInfo).Assembly);
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddCqrs(new[] { typeof(CatalogApplicationAssemblyInfo).Assembly }, pipelines: new[]
        {
            typeof(RequestValidationBehavior<,>),
            typeof(LoggingBehavior<,>),
            typeof(StreamLoggingBehavior<,>),
            typeof(CachingBehavior<,>),
            typeof(InvalidateCachingBehavior<,>),
            typeof (EfTxBehavior<,>),
        });

        return builder;
    }
}

