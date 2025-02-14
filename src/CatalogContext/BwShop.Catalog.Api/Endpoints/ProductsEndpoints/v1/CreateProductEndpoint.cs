using Ardalis.GuardClauses;
using AutoMapper;
using Bw.Core.Cqrs.Commands;
using Bw.Core.Web.MinimalApi;
using BwShop.Catalog.Application.Features.Commands.Products.CreateProduct;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Swashbuckle.AspNetCore.Annotations;

namespace BwShop.Catalog.Api.Endpoints.ProductsEndpoints.v1;

internal class CreateProductEndpoint : ICommandMinimalEndpoint<CreateProductDto>
{
    public string GroupName => CatalogEndpointsConfig.Tag;

    public string PrefixRoute => CatalogEndpointsConfig.ProductPrefixUri;

    public double Version => 1.0;

    public async Task<IResult> HandleAsync(
        HttpContext context,
        CreateProductDto request,
        ICommandProcessor commandProcessor,
        IMapper mapper,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));
        var command = new CreateProductCommand(request.Name, request.Description, request.CategoryId);
        using (Serilog.Context.LogContext.PushProperty("Endpoint", nameof(CreateProductEndpoint)))
        {
            var result = await commandProcessor.SendAsync(command, cancellationToken);
            return Results.Created($"{CatalogEndpointsConfig.ProductPrefixUri}/{result.Id}", result);
        }

    }

    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder builder)
        => builder
        .MapPost("/create", HandleAsync)
        .AllowAnonymous()
        .Produces<ProductResponseDto>(StatusCodes.Status201Created)
        .Produces<StatusCodeProblemDetails>(StatusCodes.Status400BadRequest)
        .WithMetadata(new SwaggerOperationAttribute("Creating a Product", "Creating a Product"))
        .WithName("CreateProduct")
        .WithDisplayName("Create New Product.");

}
