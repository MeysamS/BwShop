using BwShop.Media.Infrastructure.services;
using Minio;
using Minio.DataModel.Args;

namespace MediaContext.Unit.Fixtures;

public class MinioTestFixture : IDisposable
{
    public MinioMediaStorageService StorageService { get; }
    private readonly IMinioClient _minioClient;

    private const string BucketName = "test-media";

    public MinioTestFixture()
    {
        _minioClient = new MinioClient()
                        .WithCredentials("minioadmin", "minioadmin")
                        .WithEndpoint("localhost:9000")
                        .Build();

        _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(BucketName)).Wait();
        StorageService = new MinioMediaStorageService(_minioClient, BucketName);
    }


    public void Dispose()
    {
        _minioClient.RemoveBucketAsync(new RemoveBucketArgs().WithBucket(BucketName)).Wait();

    }
}