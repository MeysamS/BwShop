using Bw.Core.Cqrs.Commands;
using BwShop.Catalog.Domain.Services;
using MediatR;

namespace BwShop.Catalog.Application.Features.Commands.Products.ProductReviews;

public class AddProductReviewCommandHandler(ProductReviewService productReviewService)
    : ICommandHandler<AddProductReviewCommand>
{

    public async Task<Unit> Handle(AddProductReviewCommand request, CancellationToken cancellationToken)
    {
        await productReviewService.AddReview(request.ProductId, request.Review);
        return Unit.Value;
    }
}
