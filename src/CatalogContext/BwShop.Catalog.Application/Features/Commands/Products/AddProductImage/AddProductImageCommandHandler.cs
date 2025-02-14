using Bw.Core.Cqrs.Commands;
using Bw.Core.Persistence;
using BwShop.Catalog.Domain.Models.Entities;
using BwShop.Catalog.Domain.Repositories;
using BwShop.Catalog.Domain.Services;
using MediatR;

namespace BwShop.Catalog.Application.Features.Commands.Products.AddProductImage;

public class AddProductImageCommandHandler(
    ProductImageService productImageService
    ) : ICommandHandler<AddProductImageCommand>
{
    public async Task<Unit> Handle(AddProductImageCommand request, CancellationToken cancellationToken)
    {
        var image = new ProductImage(Guid.NewGuid(), request.ImageUrl, request.IsThumbnail);
        await productImageService.AddImage(request.ProductId, image);
        return Unit.Value;
    }
}