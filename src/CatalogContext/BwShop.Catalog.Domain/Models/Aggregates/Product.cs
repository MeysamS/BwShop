using Bw.Domain;
using Bw.Domain.Model;
using BwShop.Catalog.Domain.Models.Entities;
using BwShop.Catalog.Domain.Models.Enums;
using BwShop.Catalog.Domain.Models.ValueObjects;


namespace BwShop.Catalog.Domain.Models.Aggregates;


public class Product : Aggregate<Guid>
{
    public string Name { get; private set; }
    public string Slug { get; private set; }
    public ProductDescription Description { get; private set; }
    public ProductStatus Status { get; private set; }
    public Guid CategoryId { get; private set; }


    public bool IsInStock => Variants.Any(v => v.StockQuantity > 0);
    private readonly List<Tag> _tags = new();
    private readonly List<ProductAttribute> _attributes = new();
    private readonly List<ProductImage> _images = new();
    private readonly List<ProductVariant> _variants = new();
    private readonly List<Review> _reviews = new();
    public List<Category> Categories { get; private set; } = new();


    public IReadOnlyCollection<ProductVariant> Variants => _variants.AsReadOnly();
    public IReadOnlyCollection<Review> Reviews => _reviews.AsReadOnly();
    public IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();
    public IReadOnlyCollection<ProductAttribute> Attributes => _attributes.AsReadOnly();
    public IReadOnlyCollection<ProductImage> Images => _images.AsReadOnly();

    protected Product() { } // for EF

    private Product(string name, ProductDescription description, Guid categoryId)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty.", nameof(name));
        if (categoryId == Guid.Empty) throw new ArgumentException("CategoryId is required.", nameof(categoryId));

        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        CategoryId = categoryId;
        Status = ProductStatus.Active;
    }

    // Static Factory Method
    public static Product Create(string name, ProductDescription description, Guid categoryId)
    {
        return new Product(name, description, categoryId);
    }

    public void Publish() => Status = ProductStatus.Published;
    public void Archive() => Status = ProductStatus.Archived;


    public void UpdateDetails(string name, ProductDescription description, Guid categoryId)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty.", nameof(name));
        if (categoryId == Guid.Empty) throw new ArgumentException("CategoryId is required.", nameof(categoryId));

        Name = name;
        Description = description;
        CategoryId = categoryId;
    }

    public void AddTag(string tag)
    {
        if (_tags.Any(t => t.Value == tag)) throw new InvalidOperationException("Tag already exists.");
        _tags.Add(new Tag(tag));
    }

    public void AddAttribute(string name, string value)
    {
        if (_attributes.Any(a => a.Name == name)) throw new InvalidOperationException("Attribute already exists.");
        _attributes.Add(new ProductAttribute(name, value));
    }

    public void AddImage(ProductImage image)
    {
        if (image == null)
            throw new ArgumentNullException(nameof(image));

        if (image.IsThumbnail && Images.Any(img => img.IsThumbnail))
            throw new InvalidOperationException("A thumbnail already exists.");

        _images.Add(image);
    }

    public void SetThumbnail(Guid imageId)
    {
        var image = Images.FirstOrDefault(img => img.Id == imageId);
        if (image == null)
            throw new ArgumentException("Image not found.");

        // Remove thumbnail status from other images
        foreach (var img in Images)
        {
            if (img.IsThumbnail)
                img.RemoveThumbnail();
        }

        image.SetAsThumbnail();
    }

    public void SetStatus(ProductStatus status)
    {
        Status = status;
    }

    public void AddVariant(ProductVariant variant)
    {
        if (variant == null)
            throw new ArgumentNullException(nameof(variant));

        var existingVariant = _variants.FirstOrDefault(v => v.Color == variant.Color && v.Size == variant.Size);

        if (existingVariant != null)
        {
            existingVariant.IncreaseStock(variant.StockQuantity);
        }
        else
        {
            _variants.Add(variant);
        }
    }

    public void AddReview(Review review)
    {
        if (review == null)
            throw new ArgumentNullException(nameof(review));

        _reviews.Add(review);
    }


    public void AssignCategory(Category category, int displayOrder)
    {
        if (!Categories.Any(c => c.Id == category.Id))
        {
            Categories.Add(Category.Create(category.Name,category.Description,displayOrder, category.Id));
        }
    }
}