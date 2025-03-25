namespace BwShop.Catalog.Domain.Models.Exceptions;

public class CategorySelfReferenceException(Guid categoryId)
    : InvalidOperationException("A category cannot reference itself as a parent or sub-category.")
{
    public Guid CategoryId { get; } = categoryId;
}
