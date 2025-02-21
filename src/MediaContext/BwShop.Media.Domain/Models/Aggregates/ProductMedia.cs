using Bw.Domain.Model;
using BwShop.Media.Domain.Models.Entities;

namespace BwShop.Media.Domain.Models.Aggregates;

public class ProductMedia : Aggregate<Guid>
{
    private readonly HashSet<MediaFile> _mediaFiles = new();
    public Guid ProductId { get; private set; }
    public IReadOnlyCollection<MediaFile> MediaFiles => _mediaFiles;

    protected ProductMedia() { } // for EF Core

    private ProductMedia(Guid productId)
    {
        if (productId == Guid.Empty)
            throw new ArgumentException("ProductId cannot be empty.", nameof(productId));

        ProductId = productId;

    }

    public static ProductMedia Create(Guid productId)
    {
        return new ProductMedia(productId);
    }

    public void AddMedia(MediaFile mediaFile)
    {
        if (mediaFile == null)
            throw new ArgumentNullException(nameof(mediaFile));

        _mediaFiles.Add(mediaFile);
    }

    public void RemoveMedia(Guid mediaId)
    {
        var media = _mediaFiles.FirstOrDefault(m => m.Id == mediaId);
        if (media == null)
            throw new ArgumentException("Media file not found.");

        _mediaFiles.Remove(media);
    }

    public void AddMediaBatch(IEnumerable<MediaFile> mediaFiles)
    {
        if (mediaFiles == null || !mediaFiles.Any())
            throw new ArgumentException("Media files cannot be empty.");

        foreach (var mediaFile in mediaFiles)
        {
            AddMedia(mediaFile);
        }
    }

    public void RemoveMediaBatch(IEnumerable<Guid> mediaIds)
    {
        if (mediaIds == null || !mediaIds.Any())
            throw new ArgumentException("Media IDs cannot be empty.");


        foreach (var mediaId in mediaIds)
        {
            RemoveMedia(mediaId);
        }
    }
}