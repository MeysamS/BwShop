using Bw.Domain.Model;

namespace BwShop.Product.Domain.Models.ValueObjects;

public class Image: ValueObject
{
    public string Url { get; }

    public Image(string url)
    {
        if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
        {
            throw new ArgumentException("Invalid image URL.", nameof(url));
        }

        Url = url;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Url;
    }

    public override string ToString()
    {
        return Url;
    }
}