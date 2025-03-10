using Bw.Extensions;
using Bw.Web.Extensions;

using Microsoft.AspNetCore.Builder;
using Bw.Logging.Extensions;
using Microsoft.Extensions.DependencyInjection;
using BwShop.Reviews.Application;
using Bw.Extensions.Cqrs;
using Bw.Logging;
using Bw.Caching.Behaviours;
using Bw.Persistence.Mongo;
using Bw.Validation;
using Bw.Swagger;
using BwShop.Reviews.Api;
using Sic.Chekam.Xamin.Uaa.Config.WebApplicationBuilderExtensions;
using Bw.Caching;
using System.Reflection;

namespace BwShop.Reviews.Infrastructure.RegisterServices.WebApplicationBuilderExtensions;

public static partial class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder RegisterInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddCore(builder.Configuration);        
        builder.AddCompression();
        builder.AddCustomProblemDetails();

        builder.AddCustomSerilog();

        DotNetEnv.Env.TraversePath().Load();

        builder.AddCustomVersioning();
        builder.AddCustomSwagger(typeof(ReviewApiAssemblyInfo).Assembly);
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddCqrs(new[] { typeof(ReviewApplicationAssemblyInfo).Assembly }, pipelines: new[]
        {
            typeof(RequestValidationBehavior<,>),
            typeof(LoggingBehavior<,>),
            typeof(StreamLoggingBehavior<,>),
            typeof(CachingBehavior<,>),
            typeof(InvalidateCachingBehavior<,>),
            typeof (MongoTxBehavior<,>),
        });

        builder.AddCustomRateLimit();

        builder.Services.AddCustomValidators(typeof(ReviewApplicationAssemblyInfo).Assembly);
        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

         builder.AddCustomCaching();
        return builder;
    }
}

