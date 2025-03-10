using Bw.Core.BaseModel;
using Bw.Core.Cqrs.Commands;
using BwShop.Reviews.Application.Models.ResultModel;
using BwShop.Reviews.Application.Services.Contract;
using BwShop.Reviews.Domain.Models.Aggregates;

namespace BwShop.Reviews.Application.Features.Commands.Create.v1;

internal class CreateReviewCommandHandler(IReviewApplicationService reviewService) : ICommandHandler<CreateReviewCommand, ResultBase>
{
    public async Task<ResultBase> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var review = Review.Create(request.ProductId, request.UserId, request.Text, request.Rating);
        await reviewService.AddReviewAsync(review.UserId, review.ProductId, review.Text.Value, review.Rating.Value);

        return new SuccessResult();
    }
}