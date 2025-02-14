using FluentValidation;

namespace BwShop.Catalog.Application.Features.Commands.Products.AddProductTag;

public class AddProductTagCommandValidator : AbstractValidator<AddProductTagCommand>
{
    public AddProductTagCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("ProductId is required.");

        RuleFor(x => x.Tag)
            .NotEmpty().WithMessage("Tag cannot be empty.")
            .MaximumLength(50).WithMessage("Tag must be less than 50 characters.");
    }
}