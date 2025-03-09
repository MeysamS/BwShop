using Ardalis.GuardClauses;
using AutoMapper;
using Bw.Core.Cqrs.Commands;
using Bw.Core.Web.MinimalApi;
using BwShop.Reviews.Api;
using BwShop.Reviews.Api.Dtos;
using BwShop.Reviews.Application.Features.Commands.Delete.v1;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Swashbuckle.AspNetCore.Annotations;

internal class DeleteReviewEndpoint : ICommandMinimalEndpoint<DeleteReviewDto>
{
    public string GroupName => ReviewRoutesConfig.Tag;

    public string PrefixRoute => ReviewRoutesConfig.ReviewPrefixUri;

    public double Version => 1.0;

    public async Task<IResult> HandleAsync(
        HttpContext context,
        DeleteReviewDto request,
        ICommandProcessor commandProcessor,
        IMapper mapper,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));
        var command = new DeleteReviewCommand(request.ReviewId);

        using (Serilog.Context.LogContext.PushProperty("Endpoint", nameof(DeleteReviewEndpoint)))
        {
            var result = await commandProcessor.SendAsync(command, cancellationToken);
            return result.Success ? Results.Ok(result) : Results.NotFound(result);
        }
    }

    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder builder)
        => builder
        .MapDelete("/{reviewId:guid}", HandleAsync)
        .Produces(StatusCodes.Status200OK)
        .Produces<StatusCodeProblemDetails>(StatusCodes.Status404NotFound)
        .WithMetadata(new SwaggerOperationAttribute("Delete a Review", "Deletes an existing review"))
        .WithName("DeleteReview")
        .WithDisplayName("Delete Review.");
}