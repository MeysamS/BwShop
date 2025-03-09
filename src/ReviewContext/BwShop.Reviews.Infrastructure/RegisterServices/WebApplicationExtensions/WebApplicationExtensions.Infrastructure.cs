using Bw.Logging.Enrichers;
using Bw.Web.Extensions;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Serilog;

namespace BwShop.Reviews.Infrastructure.RegisterServices.WebApplicationExtensions;

public static partial class WebApplicationExtensions
{
    public static void UseInfrastructure(this WebApplication app)
    {
        // this middleware should be first middleware
        // request logging just log in information level and above as default
        app.UseSerilogRequestLogging(opts =>
        {
            opts.EnrichDiagnosticContext = LogEnricher.EnrichFromRequest;

            // this level wil use for request logging
            // https://andrewlock.net/using-serilog-aspnetcore-in-asp-net-core-3-excluding-health-check-endpoints-from-serilog-request-logging/#customising-the-log-level-used-for-serilog-request-logs
            opts.GetLevel = LogEnricher.GetLogLevel;
        });

        // uaa for middlewares is important and problemDetails middleware should be placed on top
        app.UseProblemDetails();
        app.UseRequestLogContextMiddleware();
        //app.UseUaaAuthentication();
        //app.UseBasicAuthentication();

        // await app.UsePostgresPersistenceMessage(app.Logger);
        app.UseCustomRateLimit();

        // if (app.Environment.IsTest() == false)
        //     app.UseCustomHealthCheck();

        // Configure the prometheus endpoint for scraping metrics
        // NOTE: This should only be exposed on an internal port!
        // .RequireHost("*:9100");
        // app.MapPrometheusScrapingEndpoint();


    }
}