using Bw.Core.Cqrs.Commands;
using BwShop.Catalog.Domain.Services;
using MediatR;

namespace BwShop.Catalog.Application.Features.Commands.Products.SetProductThumbnail;

// public class SetProductThumbnailCommandHandler(ProductImageService productImageService) : ICommandHandler<SetProductThumbnailCommand>
// {        
//
//     public async Task<Unit> Handle(SetProductThumbnailCommand request, CancellationToken cancellationToken)
//     {
//         await productImageService.SetThumbnail(request.ProductId, request.ImageId);
//         return Unit.Value;
//     }
// }