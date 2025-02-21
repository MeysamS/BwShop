using Bw.Domain.Model;

namespace BwShop.Catalog.Domain.Models.ValueObjects;

public class ProductAttribute : ValueObject
{
    public string Name { get; }
    public string Value { get; }

    public ProductAttribute(string name, string value)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Attribute name cannot be empty.", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Attribute value cannot be empty.", nameof(value));
        }

        Name = name;
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name.ToLower(); // Case-insensitive comparison
        yield return Value.ToLower(); // Case-insensitive comparison
    }

    public override string ToString()
    {
        return $"{Name}: {Value}";
    }
}
