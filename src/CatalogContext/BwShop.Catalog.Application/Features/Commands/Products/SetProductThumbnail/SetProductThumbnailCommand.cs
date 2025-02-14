using Bw.Core.Cqrs.Commands;

namespace BwShop.Catalog.Application.Features.Commands.Products.SetProductThumbnail;

public record SetProductThumbnailCommand(Guid ProductId, Guid ImageId) : ICreateCommand;
