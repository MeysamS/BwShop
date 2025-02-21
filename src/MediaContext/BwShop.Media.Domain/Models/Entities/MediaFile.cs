using Bw.Domain.Model;
using BwShop.Media.Domain.Models.ValueObjects;

namespace BwShop.Media.Domain.Models.Entities;

public class MediaFile : Entity<Guid>
{
    public FilePath Path { get; private set; }
    public MediaType Type { get; private set; }
    public MediaMetadata Metadata { get; private set; }

    protected MediaFile() { } // for EF Core

    private MediaFile(Guid id, FilePath path, MediaType type, MediaMetadata metadata)
    {

        if (path == null)
            throw new ArgumentNullException(nameof(path));

        if (type == null)
            throw new ArgumentNullException(nameof(type));

        if (metadata == null)
            throw new ArgumentNullException(nameof(metadata));

        Id = id;
        Path = path;
        Type = type;
        Metadata = metadata;
    }

    public static MediaFile Create(FilePath path, MediaType type, MediaMetadata metadata)
    {
        return new MediaFile(Guid.NewGuid(), path, type, metadata);
    }

    public Stream GetStream()
    {
        // Logic to get the file stream (e.g., from a local file or memory stream)
        return new FileStream(Path.Value, FileMode.Open, FileAccess.Read);
    }


    public void UpdatePath(FilePath newPath)
    {
        if (newPath == null)
            throw new ArgumentNullException(nameof(newPath));

        Path = newPath;
    }
}