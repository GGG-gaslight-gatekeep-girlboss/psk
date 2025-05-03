using CoffeeShop.BusinessLogic.Common.DTOs;
using CoffeeShop.BusinessLogic.Common.Exceptions;
using CoffeeShop.BusinessLogic.Common.Interfaces;
using CoffeeShop.BusinessLogic.ProductManagement.DTOs;
using CoffeeShop.BusinessLogic.ProductManagement.Entities;
using CoffeeShop.BusinessLogic.ProductManagement.Interfaces;

namespace CoffeeShop.BusinessLogic.ProductManagement.Services;

public class ProductImageService : IProductImageService
{
    private static readonly string[] AllowedImageExtensions = [".jpg", ".jpeg", ".png", ".webp", ".bmp"];

    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBlobStorage _blobStorage;

    public ProductImageService(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork,
        IBlobStorage blobStorage)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _blobStorage = blobStorage;
    }

    public async Task SetProductImage(SetProductImageDTO setProductImageDTO)
    {
        var imageExtension = Path.GetExtension(setProductImageDTO.FileName);
        ValidateImageFormat(imageExtension);
        ValidateImageSize(setProductImageDTO.ImageSize);
        
        var product = await _productRepository.GetWithImage(setProductImageDTO.ProductId);
        var productImage = ProductImage.Create(product, imageExtension);
        product.SetImage(productImage);

        var uploadImageDTO = new UploadImageDTO
        {
            Key = productImage.BlobKey,
            ContentType = setProductImageDTO.ContentType,
            ImageStream = setProductImageDTO.ImageStream
        };

        await _blobStorage.UploadImage(uploadImageDTO);
        await _unitOfWork.SaveChanges();
    }
    
    private static void ValidateImageFormat(string imageExtension)
    {
        if (!AllowedImageExtensions.Contains(imageExtension))
        {
            throw new InvalidDomainValueException(
                $"Image file format is not allowed. Please upload a valid image: {string.Join(", ", AllowedImageExtensions)}.");
        }
    }

    private static void ValidateImageSize(long imageSize)
    {
        if (imageSize > Constants.MaxImageSizeInBytes)
        {
            throw new InvalidDomainValueException("Image is too large. Please upload a smaller image.");
        }
    }
}