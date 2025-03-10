using Bw.Core.Cqrs.Commands;
using BwShop.Media.Application.Features.Commands;
using BwShop.Media.Domain.Models.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace BwShop.Media.Api.Controllers.ProductMedia;

[ApiController]
[Route("api/[controller]")]
public class ProductMediaController(ICommandProcessor commandProcessor) : ControllerBase
{
    [HttpPost("{productMediaId}/media-files")]
    public async Task<IActionResult> AddMediaFile(Guid productMediaId, IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("File is required.");

        // Extract file metadata
        var width = 800; // Example: Get width from file metadata (e.g., using a library like ImageSharp)
        var height = 600; // Example: Get height from file metadata
        var format = Path.GetExtension(file.FileName).TrimStart('.'); // Example: Get format from file extension
        var sizeInBytes = file.Length;
        var duration = (TimeSpan?)null; // Example: For videos, extract duration using a library like FFmpeg

        // Create MediaMetadata
        var metadata = MediaMetadata.Create(width, height, format, sizeInBytes, duration);

        // Determine MediaType based on file format
        var mediaType = format.ToLower() switch
        {
            "jpg" or "jpeg" or "png" or "gif" => MediaType.Image,
            "mp4" or "avi" or "mkv" => MediaType.Video,
            _ => MediaType.Document
        };

        // Upload the file
        using var fileStream = file.OpenReadStream();
        var command = new AddMediaFileCommand(productMediaId, fileStream, file.Name, mediaType, metadata);
        await commandProcessor.SendAsync(command);
        return Ok();
    }
}