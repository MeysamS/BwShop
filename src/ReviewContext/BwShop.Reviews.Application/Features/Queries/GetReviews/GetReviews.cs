using Bw.Core.Cqrs;
using Bw.Core.Cqrs.Query;
using Bw.Persistence.Mongo;
using BwShop.reviews.Application.Features.Queries.GetReviewsByProduct;
using BwShop.Reviews.Domain.Models.Repositories;

namespace BwShop.Reviews.Application.Features.Queries.GetReviews;

public record GetReviews : ListQuery<ListResultModel<ReviewResponseDTO>>;

public class GetReviewsHandler(IReviewRepository reviewRepository) : IQueryHandler<GetReviews, ListResultModel<ReviewResponseDTO>>
{
    public async Task<ListResultModel<ReviewResponseDTO>> Handle(GetReviews request, CancellationToken cancellationToken)
    {
        var reviews = await reviewRepository.GetReviewsQueryable()
            .ApplyPagingAsync(
                request.Page,
                request.PageSize,
                cancellationToken
            );

        var reviewDtos = reviews.Items.Select(review => new ReviewResponseDTO(
                   review.Id,
                   review.UserId,
                   review.Text.Value, 
                   review.Rating.Value,
                   review.CreatedAt
               )).ToList();
        return ListResultModel<ReviewResponseDTO>.Create(reviewDtos, reviews.TotalItems, request.Page, request.PageSize);

    }
}
