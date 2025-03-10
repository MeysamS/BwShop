using Ardalis.GuardClauses;
using AutoMapper;
using Bw.Core.Cqrs.Commands;
using Bw.Core.Web.MinimalApi;
using BwShop.Reviews.Api.Dtos;
using BwShop.Reviews.Application.Dtos;
using BwShop.Reviews.Application.Features.Commands.Create.v1;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Swashbuckle.AspNetCore.Annotations;

namespace BwShop.Reviews.Api.Endpoints.v1;

internal class CreateReviewEndpoint : ICommandMinimalEndpoint<CreateReviewRequest>
{
    public string GroupName => ReviewRoutesConfig.Tag;

    public string PrefixRoute => ReviewRoutesConfig.ReviewPrefixUri;

    public double Version => 1.0;

    public async Task<IResult> HandleAsync(
          HttpContext context,
          CreateReviewRequest request,
          ICommandProcessor commandProcessor,
          IMapper mapper,
          CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        var userId = Guid.Empty;// context.User.GetUserId(); // دریافت `UserId` از توکن  
        var command = new CreateReviewCommand(userId, request.ProductId, request.Text, request.Rating);

        using (Serilog.Context.LogContext.PushProperty("Endpoint", nameof(CreateReviewEndpoint)))
        {
            var result = await commandProcessor.SendAsync(command, cancellationToken);
            return result.Success ? Results.Created($"{ReviewRoutesConfig.ReviewPrefixUri}/{result.Success}", result) : Results.BadRequest(result);
        }
    }
    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder builder)
     => builder
        .MapPost("/create", HandleAsync)
        .Produces<ReviewResponseDto>(StatusCodes.Status201Created)
        .Produces<StatusCodeProblemDetails>(StatusCodes.Status400BadRequest)
        .WithMetadata(new SwaggerOperationAttribute("Create a Review", "Creates a new review for a product"))
        .WithName("CrvRev")
        .WithDisplayName("Create New Review.");
}