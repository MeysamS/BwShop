using Bw.Core.Cqrs.Commands;

namespace BwShop.Catalog.Application.Features.Commands.Products.AddProductTag;

public record AddProductTagCommand(Guid ProductId, string Tag) : ICommand;
