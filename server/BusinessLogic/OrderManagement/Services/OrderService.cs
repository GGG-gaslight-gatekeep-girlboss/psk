using CoffeeShop.BusinessLogic.OrderManagement.Interfaces;
using CoffeeShop.BusinessLogic.OrderManagement.DTOs;
using CoffeeShop.BusinessLogic.Common.Interfaces;
using CoffeeShop.BusinessLogic.Common.Exceptions;
using CoffeeShop.BusinessLogic.ProductManagement.Interfaces;
using CoffeeShop.BusinessLogic.OrderManagement.Enums;
using CoffeeShop.BusinessLogic.OrderManagement.Entities;

namespace CoffeeShop.BusinessLogic.OrderManagement.Services;

public class OrderService : IOrderService {
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IProductService _productService;
    private readonly IOrderMappingService _orderMappingService;
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        IProductService productService,
        IOrderMappingService orderMappingService,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _productService = productService;
        _orderMappingService = orderMappingService;
        _unitOfWork = unitOfWork;
    }

    public async Task<OrderDTO> CreateOrder(CreateOrderDTO dto){
        ValidatePickupTime(dto.PickupTime);
        await ValidateOrderItems(dto.Items);

        var order = _orderMappingService.MapCreateOrderDTOToOrder(dto, Status.Pending.ToString());
        await UpdateProductStock(order.Items);
        _orderRepository.Add(order);
        await _unitOfWork.SaveChanges();

        List<OrderItemDTO> mappedItems = new();
        foreach(var item in dto.Items){
            var productDTO = await _productService.GetProduct(item.ProductId);
            mappedItems.Add(new OrderItemDTO(
                productDTO,
                item.Quantity
            ));
        }
        return _orderMappingService.MapOrderToOrderDTO(order, mappedItems);
    }

    private void ValidatePickupTime(DateTimeOffset pickupTime){
        var now = DateTimeOffset.UtcNow;
        if (pickupTime <= now)
            throw new InvalidDomainValueException("Pickup time must be in the future.");

        var pickupLocalTime = pickupTime.ToLocalTime().TimeOfDay;
        if (pickupLocalTime < Constants.Open || pickupLocalTime > Constants.Close)
            throw new InvalidDomainValueException($"Pickup time must be between {Constants.Open:hh\\:mm} and {Constants.Close:hh\\:mm}.");
    }

    private async Task ValidateOrderItems(List<CreateOrderItemDTO> dtos){
        foreach (var dto in dtos){
            var product = await _productRepository.Get(dto.ProductId);
            if(dto.Quantity <= 0)
                throw new InvalidDomainValueException("The order quantity must be a positive non-zero value.");

            if(product.Stock < dto.Quantity)
                throw new InvalidDomainValueException($"The order quantity of {product.Name} exceeds the available stock.");
        }
    }

    private async Task UpdateProductStock(List<OrderItem> items){
        foreach(var item in items){
            if (!item.ProductId.HasValue)
                throw new InvalidDomainValueException("ProductId cannot be null when updating stock.");

            var product = await _productRepository.Get(item.ProductId.Value); 
            product.Stock -= item.Quantity;
            _productRepository.Update(product);
        }
    }
}