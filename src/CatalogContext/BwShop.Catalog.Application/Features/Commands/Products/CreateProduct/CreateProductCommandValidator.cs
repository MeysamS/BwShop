using FluentValidation;

namespace BwShop.Catalog.Application.Features.Commands.Products.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
          .NotEmpty().WithMessage("Product name is required.")
          .MaximumLength(200).WithMessage("Product name must be less than 200 characters.");

        RuleFor(x => x.Slug)
            .NotEmpty().WithMessage("Slug is required.")
            .MaximumLength(200).WithMessage("Slug must be less than 200 characters.");

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("CategoryId is required.");
    }
}