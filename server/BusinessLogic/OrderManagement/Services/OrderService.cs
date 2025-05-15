using CoffeeShop.BusinessLogic.Common.Interfaces;
using CoffeeShop.BusinessLogic.Common.Exceptions;
using CoffeeShop.BusinessLogic.OrderManagement.Interfaces;
using CoffeeShop.BusinessLogic.OrderManagement.DTOs;
using CoffeeShop.BusinessLogic.OrderManagement.Enums;
using CoffeeShop.BusinessLogic.OrderManagement.Entities;
using CoffeeShop.BusinessLogic.ProductManagement.Interfaces;
using CoffeeShop.BusinessLogic.UserManagement.Interfaces;
using CoffeeShop.BusinessLogic.UserManagement.Enums;
using CoffeeShop.BusinessLogic.UserManagement.Exceptions;

namespace CoffeeShop.BusinessLogic.OrderManagement.Services;

public class OrderService : IOrderService {
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderMappingService _orderMappingService;
    private readonly IProductRepository _productRepository;
    private readonly ICurrentUserAccessor _currentUserAccessor;
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(
        IOrderRepository orderRepository,
        IOrderMappingService orderMappingService,
        IProductRepository productRepository,
        ICurrentUserAccessor currentUserAccessor,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _orderMappingService = orderMappingService;
        _productRepository = productRepository;
        _currentUserAccessor = currentUserAccessor;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> CreateOrder(CreateOrderDTO dto)
    {
        ValidatePickupTime(dto.PickupTime);
        await ValidateOrderItems(dto.Items);

        List<OrderItem> items = await MapCreateOrderItemDTOToOrderItem(dto.Items);
        var order = _orderMappingService.MapCreateOrderDTOToOrder(dto, items, OrderStatus.Pending);

        await UpdateProductStock(order.Items);
        _orderRepository.Add(order);

        await _unitOfWork.SaveChanges();

        return order.Id;
    }

    public async Task<List<OrderDTO>> GetAllOrders()
    {
        var orders = await _orderRepository.GetAllWithItems();

        List<OrderDTO> mappedOrders = new();
        foreach (var order in orders)
        {
            mappedOrders.Add(_orderMappingService.MapOrderToOrderDTO(order));
        }
        return mappedOrders;
    }

    public async Task<List<OrderDTO>> GetCurrentUserOrders(){
        var userId = _currentUserAccessor.GetCurrentUserId();
        var orders = await _orderRepository.GetAllByUserId(userId);

        List<OrderDTO> mappedOrders = new();
        foreach (var order in orders)
        {
            mappedOrders.Add(_orderMappingService.MapOrderToOrderDTO(order));
        }
        return mappedOrders;
    }

    public async Task<OrderDTO> GetOrder(Guid id)
    {
        var order = await _orderRepository.GetWithItems(id);
        var role =  _currentUserAccessor.GetCurrentUserRole();
        var userId = _currentUserAccessor.GetCurrentUserId();

        if(role == Roles.Client.ToString() && order.CreatedById != userId){
            throw new UserNotAuthorizedException();
        }

        return _orderMappingService.MapOrderToOrderDTO(order);
    }

    public async Task<OrderDTO> UpdateOrderStatus(Guid id, UpdateOrderDTO request)
    {
        var order = await _orderRepository.GetWithItems(id);

        try {
            OrderStatus enumStatus = Enum.Parse<OrderStatus>(request.OrderStatus.Trim(), ignoreCase: true);
            order.Status = enumStatus;
        } catch(ArgumentException) {
            throw new InvalidDomainValueException($"{request.OrderStatus} is not a valid order status.");
        }

        _orderRepository.Update(order);

        await _unitOfWork.SaveChanges();

        return _orderMappingService.MapOrderToOrderDTO(order);
    }

    public async Task DeleteOrder(Guid id)
    {
        var order = await _orderRepository.Get(id);
        
        order.IsDeleted = true;
        _orderRepository.Update(order);

        await _unitOfWork.SaveChanges();
    }

    private void ValidatePickupTime(DateTimeOffset pickupTime)
    {
        var now = DateTimeOffset.UtcNow;
        if (pickupTime <= now)
            throw new InvalidDomainValueException("Pickup time must be in the future.");

        var pickupLocalTime = TimeOnly.FromTimeSpan(pickupTime.ToLocalTime().TimeOfDay);
        if (pickupLocalTime < Constants.Open || pickupLocalTime > Constants.Close)
            throw new InvalidDomainValueException($"Pickup time must be between {Constants.Open:hh\\:mm} and {Constants.Close:hh\\:mm}.");
    }

    private async Task ValidateOrderItems(List<CreateOrderItemDTO> dtos)
    {
        foreach (var dto in dtos){
            var product = await _productRepository.Get(dto.ProductId);
            if(dto.Quantity <= 0)
                throw new InvalidDomainValueException("Order item quantity must be positive.");

            if(product.Stock < dto.Quantity)
                throw new InvalidDomainValueException($"The order quantity of {product.Name} exceeds the available stock.");
        }
    }

    private async Task UpdateProductStock(List<OrderItem> items)
    {
        foreach(var item in items){
            if (!item.ProductId.HasValue)
                throw new InvalidDomainValueException("ProductId cannot be null when updating stock.");

            var product = await _productRepository.Get(item.ProductId.Value); 
            product.Stock -= item.Quantity;
            _productRepository.Update(product);
        }
    }

    private async Task<List<OrderItem>> MapCreateOrderItemDTOToOrderItem(List<CreateOrderItemDTO> dtos){
        var items = new List<OrderItem>();
        foreach (var dto in dtos)
        {
            var product = await _productRepository.Get(dto.ProductId);
            items.Add(new OrderItem{
                ProductId = dto.ProductId,
                ProductName = product.Name,
                ProductPrice = product.Price,
                Quantity = dto.Quantity
            });
        }
        return items;
    }

}