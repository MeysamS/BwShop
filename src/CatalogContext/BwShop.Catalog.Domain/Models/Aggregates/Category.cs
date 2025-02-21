using Bw.Domain.Model;

namespace BwShop.Catalog.Domain.Models.Aggregates;

public class Category : Aggregate<Guid>
{
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public int DisplayOrder { get; private set; }

    public Guid? ParentCategoryId { get; private set; }
    private readonly List<Guid> _subCategoryIds = new();
    private readonly List<Product> _products = [];

    public IReadOnlyCollection<Guid> SubCategoryIds => _subCategoryIds.AsReadOnly();
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

    // Constructor
    private Category(string name, string description, int displayOrder, Guid? parentCategoryId = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category name cannot be empty.", nameof(name));

        Id = Guid.NewGuid();
        Name = name;
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
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category name cannot be empty.", nameof(name));

        Name = name;
        Description = description;
    }

    public void AddSubCategory(Guid subCategoryId)
    {
        if (subCategoryId == Id)
            throw new InvalidOperationException("A category cannot be its own sub-category.");

        if (!_subCategoryIds.Contains(subCategoryId))
            _subCategoryIds.Add(subCategoryId);
    }

    public void RemoveSubCategory(Guid subCategoryId)
    {
        _subCategoryIds.Remove(subCategoryId);
    }

    public void SetParentCategory(Guid parentCategoryId)
    {
        if (parentCategoryId == Id)
            throw new InvalidOperationException("A category cannot be its own parent.");

        ParentCategoryId = parentCategoryId;
    }
}