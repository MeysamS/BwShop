using Bw.Domain.Model;

namespace BwShop.Catalog.Domain.Models.ValueObjects;

public class Slug : ValueObject
{
    public string Value { get; }

    private Slug(string value)
    {
        Value = value;
    }

    public static Slug Create(string input)
    {
        var slug = input.ToLowerInvariant().Trim()
                        .Replace(" ", "-")
                        .Replace("’", "")
                        .Replace(":", "")
                        .Replace(";", "");

        return new Slug(slug);
    }


    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}