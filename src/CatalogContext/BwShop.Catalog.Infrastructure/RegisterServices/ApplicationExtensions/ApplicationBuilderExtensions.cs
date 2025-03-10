using Microsoft.AspNetCore.Builder;
namespace BwShop.Catalog.Infrastructure.RegisterServices.ApplicationExtensions;

public static class ApplicationBuilderExtensions
{
    /// <summary>
    ///     Register CORS.
    /// </summary>
    public static IApplicationBuilder UseAppCors(this IApplicationBuilder app)
    {
        app.UseCors(p =>
        {
            p.AllowAnyOrigin();
            // p.WithMethods("GET");
            p.AllowAnyMethod();
            p.AllowAnyHeader();
        });

        return app;
    }
}
