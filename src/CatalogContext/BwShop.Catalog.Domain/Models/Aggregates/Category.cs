using Bw.Domain.Model;
using BwShop.Catalog.Domain.Models.Exceptions;

namespace BwShop.Catalog.Domain.Models.Aggregates;

public class Category : Aggregate<Guid>
{
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public int DisplayOrder { get; private set; }

    public Guid? ParentCategoryId { get; private set; }
    private readonly HashSet<Guid> _subCategoryIds = [];
    private readonly List<Product> _products = [];

    public IReadOnlySet<Guid> SubCategoryIds => _subCategoryIds;
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

    // Constructor
    private Category(string name, string description, int displayOrder, Guid? parentCategoryId = null)
    {
        Id = Guid.NewGuid();
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
        Description = description;
        DisplayOrder = displayOrder;
        ParentCategoryId = parentCategoryId;
    }

    // Static Factory Method
    public static Category Create(string name, string description, int displayOrder, Guid? parentCategoryId = null)
    {
        return new Category(name, description, displayOrder, parentCategoryId);
    }

    // Methods
    public void UpdateDetails(string name, string description)
    {
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
        Description = description;
    }

    public void AddSubCategory(Guid subCategoryId)
    {
        if (subCategoryId == Id)
            throw new CategorySelfReferenceException(Id);

        _subCategoryIds.Add(subCategoryId);
    }

    public void RemoveSubCategory(Guid subCategoryId)
    {
        _subCategoryIds.Remove(subCategoryId);
    }

    public void SetParentCategory(Guid parentCategoryId)
    {
        if (parentCategoryId == Id)
            throw new CategorySelfReferenceException(Id);

        ParentCategoryId = parentCategoryId;
    }
}