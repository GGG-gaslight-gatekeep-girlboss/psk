using CoffeeShop.BusinessLogic.Common.Interfaces;
using CoffeeShop.BusinessLogic.Common.Exceptions;
using CoffeeShop.BusinessLogic.OrderManagement.Interfaces;
using CoffeeShop.BusinessLogic.OrderManagement.DTOs;
using CoffeeShop.BusinessLogic.OrderManagement.Enums;
using CoffeeShop.BusinessLogic.OrderManagement.Entities;
using CoffeeShop.BusinessLogic.ProductManagement.Interfaces;
using CoffeeShop.BusinessLogic.ProductManagement.DTOs;
using CoffeeShop.BusinessLogic.UserManagement.Interfaces;
using CoffeeShop.BusinessLogic.UserManagement.Enums;
using CoffeeShop.BusinessLogic.UserManagement.Exceptions;

namespace CoffeeShop.BusinessLogic.OrderManagement.Services;

public class OrderService : IOrderService {
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderMappingService _orderMappingService;
    private readonly IProductRepository _productRepository;
    private readonly IProductService _productService;
    private readonly ICurrentUserAccessor _currentUserAccessor;
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(
        IOrderRepository orderRepository,
        IOrderMappingService orderMappingService,
        IProductRepository productRepository,
        IProductService productService,
        ICurrentUserAccessor currentUserAccessor,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _orderMappingService = orderMappingService;
        _productRepository = productRepository;
        _productService = productService;
        _currentUserAccessor = currentUserAccessor;
        _unitOfWork = unitOfWork;
    }

    public async Task<OrderDTO> CreateOrder(CreateOrderDTO dto){
        ValidatePickupTime(dto.PickupTime);
        await ValidateOrderItems(dto.Items);

        var order = _orderMappingService.MapCreateOrderDTOToOrder(dto, Status.Pending.ToString());
        await UpdateProductStock(order.Items);
        _orderRepository.Add(order);
        await _unitOfWork.SaveChanges();

        return await MapEnrichedOrderToDTO(order);
    }

    public async Task<List<OrderDTO>> GetAllOrders(){
        var role =  _currentUserAccessor.GetCurrentUserRole();
        var userId = _currentUserAccessor.GetCurrentUserId();

        List<Order> orders = new();
        if (role == Roles.Client.ToString())
        {
            orders = await _orderRepository.GetAllByUserId(userId);
        } else {
            orders = await _orderRepository.GetAllWithItems();
        }

        List<OrderDTO> mappedOrders = new();
        foreach (var order in orders)
        {
            mappedOrders.Add(await MapEnrichedOrderToDTO(order));
        }
        return mappedOrders;
    }

    public async Task<OrderDTO> GetOrder(Guid id){
        var order = await _orderRepository.GetWithItems(id);

        var role =  _currentUserAccessor.GetCurrentUserRole();
        var userId = _currentUserAccessor.GetCurrentUserId();
        if(role == Roles.Client.ToString() && order.CreatedById != userId){
            throw new UserNotAuthorizedException();
        }

        return await MapEnrichedOrderToDTO(order);
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

    private async Task<OrderDTO> MapEnrichedOrderToDTO(Order order)
    {
        var items = new List<OrderItemDTO>();
        foreach (var item in order.Items)
        {
            if (!item.ProductId.HasValue)
            {
                var deletedProductDTO = new ProductDTO
                (
                    Guid.Empty,
                    "Deleted Product",
                    "This product is no longer available",
                    0,
                    0,
                    null
                );
                items.Add(new OrderItemDTO(
                    deletedProductDTO,
                    item.Quantity
                ));
                continue;
            }

            var productDTO = await _productService.GetProduct(item.ProductId.Value);
            items.Add(new OrderItemDTO(productDTO, item.Quantity));
        }

        return _orderMappingService.MapOrderToOrderDTO(order, items);
    }

}