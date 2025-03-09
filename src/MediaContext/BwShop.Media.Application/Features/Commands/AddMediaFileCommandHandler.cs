using Bw.Core.Cqrs.Commands;
using Bw.Core.Persistence;
using BwShop.Media.Application.Services;
using BwShop.Media.Domain.Repositories;
using MediatR;

namespace BwShop.Media.Application.Features.Commands;

public class AddMediaFileCommandHandler(
    IProductMediaRepository productMediaRepository,
    ProductMediaApplicationService productMediaApplicationService,
    IUnitOfWork unitOfWork) : ICommandHandler<AddMediaFileCommand>
{
    public async Task<Unit> Handle(AddMediaFileCommand request, CancellationToken cancellationToken)
    {
        // Get the ProductMedia entity
        var productMedia = await productMediaRepository.GetByIdAsync(request.ProductMediaId);
        if (productMedia == null)
            throw new ArgumentException("Product media not found.");

        // Upload the file and create a MediaFile
        var mediaFile = await productMediaApplicationService.UploadMediaFileAsync(
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