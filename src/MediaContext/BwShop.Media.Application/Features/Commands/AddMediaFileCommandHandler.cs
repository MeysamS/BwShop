using Bw.Core.Cqrs.Commands;
using Bw.Core.Persistence;
using BwShop.Media.Domain.Repositories;
using BwShop.Media.Domain.Services;
using MediatR;

namespace BwShop.Media.Application.Features.Commands;

public class AddMediaFileCommandHandler(
    IProductMediaRepository productMediaRepository,
    ProductMediaService productMediaService,
    IUnitOfWork unitOfWork) : ICommandHandler<AddMediaFileCommand>
{
    public async Task<Unit> Handle(AddMediaFileCommand request, CancellationToken cancellationToken)
    {
        // Get the ProductMedia entity
        var productMedia = await productMediaRepository.GetByIdAsync(request.ProductMediaId);
        if (productMedia == null)
            throw new ArgumentException("Product media not found.");

        // Upload the file and create a MediaFile
        var mediaFile = await productMediaService.UploadMediaFileAsync(
            request.FileStream,
            request.FileName,
            request.MediaType,
            request.Metadata
        );

        // Add the MediaFile to the ProductMedia
        productMedia.AddMedia(mediaFile);

        // Update the ProductMedia in the repository
        await productMediaRepository.UpdateAsync(productMedia);
        await unitOfWork.CommitAsync();
        return Unit.Value;

    }
}