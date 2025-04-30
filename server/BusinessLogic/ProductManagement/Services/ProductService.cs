using CoffeeShop.BusinessLogic.Common.DTOs;
using CoffeeShop.BusinessLogic.ProductManagement.DTOs;
using CoffeeShop.BusinessLogic.ProductManagement.Interfaces;
using CoffeeShop.BusinessLogic.Common.Interfaces;
using CoffeeShop.BusinessLogic.Common.Exceptions;
using CoffeeShop.BusinessLogic.ProductManagement.Entities;

namespace CoffeeShop.BusinessLogic.ProductManagement.Services;

public class ProductService : IProductService
{
    private static readonly string[] AllowedImageExtensions = [".jpg", ".jpeg", ".png", ".webp", ".bmp"];

    private readonly IProductRepository _productRepository;
    private readonly IProductMappingService _productMappingService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBlobStorage _blobStorage;

    public ProductService(
        IProductRepository productRepository,
        IProductMappingService productMappingService,
        IUnitOfWork unitOfWork,
        IBlobStorage blobStorage)
    {
        _productRepository = productRepository;
        _productMappingService = productMappingService;
        _unitOfWork = unitOfWork;
        _blobStorage = blobStorage;
    }

    public async Task<ProductDTO> CreateProduct(CreateProductDTO dto)
    {
        if (dto.Price < 0)
        {
            throw new InvalidDomainValueException("Price must be non-negative.");
        }

        if (dto.Stock < 0)
        {
            throw new InvalidDomainValueException("Stock must be non-negative.");
        }
        
        var product = _productMappingService.MapCreateProductDTOToProduct(dto);
        _productRepository.Add(product);

        await _unitOfWork.SaveChanges();

        return _productMappingService.MapProductToProductDTO(product);
    }

    public async Task<ProductDTO> GetProduct(Guid id)
    {
        var product = await _productRepository.GetWithImage(id);
        return _productMappingService.MapProductToProductDTO(product);
    }

     public async Task DeleteProduct(Guid id)
    {
        await _productRepository.Delete(id);
    }

    public async Task<ProductDTO> UpdateProduct(Guid id, UpdateProductDTO dto)
    {
        var product = await _productRepository.Get(id);

        if (product is null)
        {
            throw new KeyNotFoundException($"Product with id {id} is not found");
        }

        if (dto.Name is not null)
        {
            product.Name = dto.Name;
        }

        if (dto.Description is not null)
        {
            product.Description = dto.Description;
        }

        if (dto.Price.HasValue)
        {
            product.Price = dto.Price.Value;
        }

        if (dto.Stock.HasValue)
        {
            product.Stock = dto.Stock.Value;
        }

        _productRepository.Update(product);

        await _unitOfWork.SaveChanges();

        return _productMappingService.MapProductToProductDTO(product);
    }

    public async Task<List<ProductDTO>> GetAllProducts()
    {
        var products = await _productRepository.GetAllWithImages();  
        return products.Select(_productMappingService.MapProductToProductDTO).ToList();
    }

    public async Task SetProductImage(SetProductImageDTO setProductImageDTO)
    {
        var imageExtension = Path.GetExtension(setProductImageDTO.FileName);
        ValidateImageFormat(imageExtension);
        
        var product = await _productRepository.GetWithImage(setProductImageDTO.ProductId);
        var productImage = ProductImage.Create(product, imageExtension);
        product.SetImage(productImage);

        var uploadImageDTO = new UploadImageDTO
        {
            Key = productImage.BlobKey,
            ContentType = setProductImageDTO.ContentType,
            ImageStream = setProductImageDTO.Image
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
}