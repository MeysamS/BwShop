using Bw.Core.BaseModel;
using Bw.Core.Cqrs.Commands;
using Bw.Persistence.Mongo.Abstraction;
using BwShop.Reviews.Application.Models.ResultModel;
using BwShop.Reviews.Domain.Models.Repositories;

namespace BwShop.Reviews.Application.Features.Commands.Delete.v1;

public class DeleteReviewCommandHandler(IMongoUnitOfWork unitOfWork,IReviewRepository reviewRepository) : ICommandHandler<DeleteReviewCommand, ResultBase>
{

    public async Task<ResultBase> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        var review = await reviewRepository.FindByIdAsync(request.ReviewId);
        if (review == null)
            return new FailureResult("Review not found.");

        await reviewRepository.DeleteAsync(review,cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return new SuccessResult();
    }
}