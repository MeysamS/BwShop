using Bw.Core.Cqrs.Commands;
using BwShop.Catalog.Domain.Services;
using MediatR;

namespace BwShop.Catalog.Application.Features.Commands.Products.AddProductTag;

public class AddProductTagCommandHandler(ProductTagService productTagService) 
    : ICommandHandler<AddProductTagCommand>
{
    public async Task<Unit> Handle(AddProductTagCommand request, CancellationToken cancellationToken)
    {

        await productTagService.AddTag(request.ProductId, request.Tag);
        return Unit.Value;
    }

}