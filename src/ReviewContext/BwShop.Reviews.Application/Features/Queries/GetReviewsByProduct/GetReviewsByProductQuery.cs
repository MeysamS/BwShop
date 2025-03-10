using Bw.Core.Cqrs;
using Bw.Core.Cqrs.Query;
using Bw.Persistence.Mongo;
using BwShop.Reviews.Domain.Models.Repositories;

namespace BwShop.reviews.Application.Features.Queries.GetReviewsByProduct;

public record GetReviewsByProductQuery(Guid productId,int page=0,int pageSize=10) :  IQuery<ListResultModel<ReviewResponseDTO>>;



internal class GetReviewsByProductQueryHandler(IReviewRepository reviewRepository) : IQueryHandler<GetReviewsByProductQuery, ListResultModel<ReviewResponseDTO>>
{
    public async Task<ListResultModel<ReviewResponseDTO>> Handle(GetReviewsByProductQuery request, CancellationToken cancellationToken)
    {
        var reviews = await reviewRepository.GetReviewsQueryable().Where(r => r.ProductId == request.productId)
                        .ApplyPagingAsync(request.page, request.pageSize, cancellationToken);

        var reviewDtos = reviews.Items.Select(review => new ReviewResponseDTO(
                      review.Id,
                      review.UserId,
                      review.Text.Value,
                      review.Rating.Value,
                      review.CreatedAt
                  )).ToList();

        return ListResultModel<ReviewResponseDTO>.Create(reviewDtos, reviews.TotalItems, request.page, request.pageSize);
    }
}