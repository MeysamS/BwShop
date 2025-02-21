using Bw.Domain.Model;

namespace BwShop.Media.Domain.Models.ValueObjects;

public class MediaType : ValueObject
{
    public string Value { get; }

    public MediaType(string value)
    {
        Value = value;
    }

    public static MediaType Image => new("Image");
    public static MediaType Video => new("Video");
    public static MediaType Document => new("Document");

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}