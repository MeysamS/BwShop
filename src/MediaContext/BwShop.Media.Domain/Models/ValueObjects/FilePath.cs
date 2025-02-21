using Bw.Domain.Model;

namespace BwShop.Media.Domain.Models.ValueObjects;

public class FilePath : ValueObject
{
    public string Value { get; }


    public FilePath(string value)
    {
        if (!Uri.IsWellFormedUriString(value, UriKind.Absolute))
        {
            throw new ArgumentException("Invalid Path.", nameof(value));
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