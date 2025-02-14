namespace BwShop.Catalog.Application.Features.Commands.Products.CreateProduct;

public record ProductResponseDto(Guid Id,string Name,string Slug,string Description,Guid CategoryId);
