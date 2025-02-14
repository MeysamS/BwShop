using Bw.Core.Cqrs.Commands;
using BwShop.Catalog.Domain.Services;

namespace BwShop.Catalog.Application.Features.Queries.Products.GetProductAverageRating;

public class GetProductAverageRatingQueryHandler(ProductReviewService productReviewService) : ICommandHandler<GetProductAverageRatingQuery, double>
{
    public async Task<double> Handle(GetProductAverageRatingQuery request, CancellationToken cancellationToken)
    {
        return await productReviewService.CalculateAverageRating(request.ProductId);
    }
}
