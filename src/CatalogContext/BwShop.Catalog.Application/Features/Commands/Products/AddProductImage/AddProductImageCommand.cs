using System.Windows.Input;
using Bw.Core.Cqrs.Commands;

namespace BwShop.Catalog.Application.Features.Commands.Products.AddProductImage;

public record AddProductImageCommand(Guid ProductId, string ImageUrl, bool IsThumbnail) : ICreateCommand;
