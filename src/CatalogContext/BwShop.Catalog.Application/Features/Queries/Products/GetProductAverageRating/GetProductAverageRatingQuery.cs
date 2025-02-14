using Bw.Core.Cqrs.Commands;

namespace BwShop.Catalog.Application.Features.Queries.Products.GetProductAverageRating;

public record GetProductAverageRatingQuery(Guid ProductId) : ICommand<double>;
