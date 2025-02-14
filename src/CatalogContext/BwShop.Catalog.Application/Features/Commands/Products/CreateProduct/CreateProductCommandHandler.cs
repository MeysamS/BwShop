using Bw.Core.Cqrs.Commands;
using Bw.Core.Persistence;
using BwShop.Catalog.Domain.Models.Aggregates;
using BwShop.Catalog.Domain.Models.ValueObjects;
using BwShop.Catalog.Domain.Repositories;

namespace BwShop.Catalog.Application.Features.Commands.Products.CreateProduct;

public class CreateProductCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateProductCommand, ProductResponseDto>
{
    public async Task<ProductResponseDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = Product.Create(request.Name, new ProductDescription(request.Description, ""), request.CategoryId);
        await productRepository.AddAsync(product);
        await unitOfWork.CommitAsync(cancellationToken);
        return new ProductResponseDto(
           product.Id,
           product.Name,
           product.Slug.Value,
           product.Description.LongDescription,
           product.CategoryId
       );
    }
}