using Bw.Core.Cqrs.Commands;
using BwShop.Media.Domain.Models.ValueObjects;

namespace BwShop.Media.Application.Features.Commands;

public class AddMediaFileCommand : ICommand
{
    public Guid ProductMediaId { get; }
    public Stream FileStream { get; }
    public string FileName { get; }
    public MediaType MediaType { get; }
    public MediaMetadata Metadata { get; }

    public AddMediaFileCommand(Guid productMediaId, Stream fileStream, string fileName, MediaType mediaType, MediaMetadata metadata)
    {
        if (productMediaId == Guid.Empty)
            throw new ArgumentException("ProductMediaId cannot be empty.", nameof(productMediaId));

        if (fileStream == null)
            throw new ArgumentNullException(nameof(fileStream));

        if (string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentException("File name cannot be empty.", nameof(fileName));

        if (metadata == null)
            throw new ArgumentNullException(nameof(metadata));

        ProductMediaId = productMediaId;
        FileStream = fileStream;
        FileName = fileName;
        MediaType = mediaType;
        Metadata = metadata;
    }
}