using Bw.Core.Cqrs.Commands;
using BwShop.Catalog.Domain.Models.Aggregates;

namespace BwShop.Catalog.Application.Features.Commands.Products.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Description,
    Guid CategoryId) : ICreateCommand<ProductResponseDto>;