using Ardalis.GuardClauses;
using Asp.Versioning;
using Bw.Core.Cqrs;
using Bw.Web;
using BwShop.reviews.Application.Features.Queries.GetReviewsByProduct;
using BwShop.Reviews.Api.Dtos;
using BwShop.Reviews.Application.Features.Commands.Create.v1;
using BwShop.Reviews.Application.Features.Commands.Delete.v1;
using BwShop.Reviews.Application.Features.Queries.GetReviews;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BwShop.Reviews.Api.Controllers;

[ApiController]
[ApiVersion(1.0)]
public class ReviewController : BaseController
{


    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(StatusCodeProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(StatusCodeProblemDetails), StatusCodes.Status400BadRequest)]
    [SwaggerOperation(
        Summary = "create review",
        Description = "create review",
        OperationId = "CreateReview",
        Tags = new[] { ReviewRoutesConfig.Tag }
        )]
    [HttpPost($"{ReviewRoutesConfig.CreateReview}", Name = "CreateReview")]
    public async Task<IActionResult> Create([FromBody] CreateReviewRequest request, CancellationToken cancellationToken = default)
    {

        Guard.Against.Null(request, nameof(request));

        var userId = Guid.NewGuid();// context.User.GetUserId(); // دریافت `UserId` از توکن  
        var command = new CreateReviewCommand(userId, request.ProductId, request.Text, request.Rating);

        using (Serilog.Context.LogContext.PushProperty("Endpoint", nameof(Create)))
        {
            var result = await CommandProcessor.SendAsync(command, cancellationToken);
            return Ok(result.Success ? Results.Created($"{ReviewRoutesConfig.ReviewPrefixUri}/{result.Success}", result) : Results.BadRequest(result));
        }
    }


    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(StatusCodeProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(StatusCodeProblemDetails), StatusCodes.Status400BadRequest)]
    [SwaggerOperation(
        Summary = "delete review",
        Description = "delete review",
        OperationId = "DeleteReview",
        Tags = new[] { ReviewRoutesConfig.Tag }
        )]
    [HttpDelete($"{ReviewRoutesConfig.DeleteeReview}/{{id}}", Name = "DeleteReview")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(id, nameof(id));
        var userId = Guid.NewGuid();// context.User.GetUserId(); // دریافت `UserId` از توکن  
        var command = new DeleteReviewCommand(id);
        var result = await CommandProcessor.SendAsync(command, cancellationToken);
        return Ok(result.Success);
    }



    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(StatusCodeProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(StatusCodeProblemDetails), StatusCodes.Status400BadRequest)]
    [SwaggerOperation(
        Summary = "Get Reviews",
        Description = "Get Reviews By ProductId",
        OperationId = "GetReviewbyProductId",
        Tags = new[] { ReviewRoutesConfig.Tag }
        )]
    [HttpGet(ReviewRoutesConfig.ReviewsByProductId, Name = "getReviewByProductId")]
    public async Task<ActionResult<ListResultModel<ReviewResponseDTO>>> GetReviewsByProductId(
        [FromRoute] Guid productId,
        [FromQuery] GetReviewsByProductidRequest request)
    {
        Guard.Against.Null(request, nameof(request));
        var query = new GetReviewsByProductQuery(productId, request.Page, request.PageSize);
        using (Serilog.Context.LogContext.PushProperty("Endpoint", nameof(Create)))
        {
            var result = await QueryProcessor.SendAsync(query);
            return Ok(result);
        }
    }



    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(StatusCodeProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(StatusCodeProblemDetails), StatusCodes.Status400BadRequest)]
    [SwaggerOperation(
          Summary = "Get Reviews",
          Description = "Get Reviews",
          OperationId = "GetReviews",
          Tags = new[] { ReviewRoutesConfig.Tag }
          )]
    [HttpGet(ReviewRoutesConfig.Reviews, Name = "GetReviews")]
    public async Task<ActionResult<ListResultModel<ReviewResponseDTO>>> GetReviews(
          [FromQuery] GetReviewsRequest? request,
          CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(request, nameof(request));

        using (Serilog.Context.LogContext.PushProperty("Endpoint", "GetReviews"))
        {
            var result = await QueryProcessor.SendAsync(
                new GetReviews
                {
                    Page = request.Page,
                    PageSize = request.PageSize
                },
                cancellationToken
            );
            return Ok(result);
        }
    }
}