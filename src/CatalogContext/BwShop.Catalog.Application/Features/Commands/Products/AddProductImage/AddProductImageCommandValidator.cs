using FluentValidation;

namespace BwShop.Catalog.Application.Features.Commands.Products.AddProductImage;

public class AddProductImageCommandValidator : AbstractValidator<AddProductImageCommand>
{
    public AddProductImageCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("ProductId is required.");

        RuleFor(x => x.ImageUrl)
            .NotEmpty().WithMessage("ImageUrl cannot be empty.")
            .Matches(@"^https?:\/\/.*\.(jpg|jpeg|png|gif)$").WithMessage("Invalid image URL format.");

        RuleFor(x => x.IsThumbnail)
            .NotNull().WithMessage("IsThumbnail must be specified.");
    }
}