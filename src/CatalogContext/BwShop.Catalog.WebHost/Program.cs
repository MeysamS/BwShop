using Bogus;
using Bw.Extensions.Microsoft.DependencyInjection;
using Bw.Extensions.Web;
using Bw.Swagger;
using Bw.Web;
using Bw.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Spectre.Console;

AnsiConsole.Write(new FigletText("Catalog Bounded Context Hosting").Centered().Color(Color.FromInt32(new Faker().Random.Int(1, 255))));

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseDefaultServiceProvider(
    (context, options) =>
    {
        options.ValidateScopes =
          context.HostingEnvironment.IsDevelopment()
          || context.HostingEnvironment.IsTest()
          || context.HostingEnvironment.IsStaging();
    }
);

builder.Services.AddControllers(
                    options => options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()))
                ).AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

                }).AddControllersAsServices();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddValidatedOptions<AppOptions>();

builder.AddMinimalEndpoints();

builder.AddModulesServices();
var app = builder.Build();
await app.ConfigureModules();
app.UseCors();

// https://learn.microsoft.com/en-us/aspnet/core/diagnostics/asp0014
app.MapControllers();

/*----------------- Module Routes Setup ------------------*/
app.MapModulesEndpoints();

// map registered minimal endpoints
app.MapMinimalEndpoints();
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("docker"))
{
    // swagger middleware should register last to discover all endpoints and its versions correctly
    app.UseCustomSwagger();
}


await app.RunAsync();
