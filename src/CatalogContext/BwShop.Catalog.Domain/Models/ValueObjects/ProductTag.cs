using Bw.Domain.Model;

namespace BwShop.Catalog.Domain.Models.ValueObjects;

public class ProductTag : ValueObject
{
    public string Value { get; }

    public ProductTag(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Tag cannot be empty.", nameof(value));
        }

        Value = value;
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value;
    }
}