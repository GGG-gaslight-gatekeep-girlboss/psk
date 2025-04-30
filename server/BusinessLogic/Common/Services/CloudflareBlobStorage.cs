using Amazon.S3;
using Amazon.S3.Model;
using CoffeeShop.BusinessLogic.Common.DTOs;
using CoffeeShop.BusinessLogic.Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace CoffeeShop.BusinessLogic.Common.Services;

public class CloudflareBlobStorage : IBlobStorage
{
    private readonly AmazonS3Client _s3Client;
    private readonly string _bucketName;

    public CloudflareBlobStorage(AmazonS3Client s3Client, IConfiguration configuration)
    {
        _s3Client = s3Client;
        _bucketName = configuration["BlobStorage:BucketName"]!;
    }

    public async Task UploadImage(UploadImageDTO uploadImageDTO)
    {
        var putObjectRequest = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = uploadImageDTO.Key,
            InputStream = uploadImageDTO.ImageStream,
            ContentType = uploadImageDTO.ContentType,
            DisablePayloadSigning = true
        };

        await _s3Client.PutObjectAsync(putObjectRequest);
    }
}