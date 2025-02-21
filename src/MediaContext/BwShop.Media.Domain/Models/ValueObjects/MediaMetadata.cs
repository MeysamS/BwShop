using Bw.Domain.Model;

namespace BwShop.Media.Domain.Models.ValueObjects;

public class MediaMetadata : ValueObject
{
    public int? Width { get; private set; }
    public int? Height { get; private set; }
    public string Format { get; private set; }
    public long SizeInBytes { get; private set; }

    public TimeSpan? Duration { get; private set; }

    private MediaMetadata() { } // for EF Core

    private MediaMetadata(int width, int height, string format, long sizeInBytes, TimeSpan? durtion)
    {
        if (width <= 0)
            throw new ArgumentException("Width must be greater than zero.", nameof(width));

        if (height <= 0)
            throw new ArgumentException("Height must be greater than zero.", nameof(height));

        if (string.IsNullOrWhiteSpace(format))
            throw new ArgumentException("Format cannot be empty.", nameof(format));

        if (sizeInBytes <= 0)
            throw new ArgumentException("Size must be greater than zero.", nameof(sizeInBytes));

        Width = width;
        Height = height;
        Format = format;
        SizeInBytes = sizeInBytes;
        Duration = durtion;
    }

    public static MediaMetadata Create(int width, int height, string format, long sizeInByte, TimeSpan? duration)
    {
        return new MediaMetadata(width, height, format, sizeInByte, duration);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Width ?? 0;
        yield return Height ?? 0;
        yield return Format;
        yield return SizeInBytes;
        yield return Duration ?? TimeSpan.Zero;
    }
}