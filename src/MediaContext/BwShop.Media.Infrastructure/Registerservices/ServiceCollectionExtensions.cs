using BwShop.Media.Domain.Services;
using BwShop.Media.Infrastructure.services;
using Microsoft.Extensions.DependencyInjection;
using Minio;

namespace BwShop.Media.Infrastructure.Registerservices;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMinioMediaStorageService(this IServiceCollection services, string endpoint, string accessKey, string secretKey, string bucketName)
    {
        var minioClient = new MinioClient()
            .WithEndpoint(endpoint)
            .WithCredentials(accessKey, secretKey)
            .Build();

        services.AddSingleton(minioClient);
        services.AddSingleton<IMediaStorageService, MinioMediaStorageService>(sp =>
            new MinioMediaStorageService(sp.GetRequiredService<MinioClient>(), bucketName));
        return services;
    }
}