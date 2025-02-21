using System.Windows.Input;
using Bw.Core.Cqrs.Commands;
using BwShop.Catalog.Domain.Models.Entities;

namespace BwShop.Catalog.Application.Features.Commands.Products.ProductReviews;

public record AddProductReviewCommand(Guid ProductId, ProductReview Review) : ITxCommand;
