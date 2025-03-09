using AutoMapper;
using Bw.Core.Cqrs.Query;
using Bw.Core.Web.MinimalApi;
using BwShop.reviews.Application.Features.Queries.GetReviewsByProduct;
using BwShop.Reviews.Application.Dtos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Swashbuckle.AspNetCore.Annotations;

namespace BwShop.Reviews.Api.Endpoints.v1;

internal class GetReviewsByProductEndpoint : IQueryMinimalEndpoint<Guid>
{
    public string GroupName => ReviewRoutesConfig.Tag;

    public string PrefixRoute => ReviewRoutesConfig.ReviewPrefixUri;

    public double Version => 1.0;

    public async Task<IResult> HandleAsync(
        HttpContext context,
        Guid productId,
        IQueryProcessor queryProcessor,
        IMapper mapper,
        CancellationToken cancellationToken)
    {
        var query = new GetReviewsByProductQuery(productId);
        var result = await queryProcessor.SendAsync(query, cancellationToken);
        return Results.Ok(result);
    }

    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder builder)
        => builder
        .MapGet("/{productId:guid}", HandleAsync)
        .Produces<IReadOnlyList<ReviewResponseDto>>(StatusCodes.Status200OK)
        .WithMetadata(new SwaggerOperationAttribute("Get Reviews by Product", "Fetches all reviews for a given product"))
        .WithName("GetReviewsByProduct")
        .WithDisplayName("Get Reviews by Product.");

}
