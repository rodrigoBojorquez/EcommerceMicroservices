using OrderApi.Application.Dtos.Orders;
using OrderApi.Domain.Entities;

namespace OrderApi.Application.Conversions;

public static class OrderConversion
{
    public static Order ToEntity(OrderDto orderDto) => new Order()
    {
        Id = orderDto.Id,
        OrderDate = orderDto.OrderDate,
        CustomerId = orderDto.CustomerId,
        ProductId = orderDto.ProductId,
        PurchaseQuantity = orderDto.PurchaseQuantity
    };
    
    public static Order ToEntity(OrderCreateDto orderCreateDto) => new Order()
    {
        Id = Guid.NewGuid(),
        OrderDate = DateTime.UtcNow,
        CustomerId = orderCreateDto.CustomerId,
        ProductId = orderCreateDto.ProductId,
        PurchaseQuantity = orderCreateDto.PurchaseQuantity
    };
    
    public static Order ToEntity(OrderUpdateDto orderUpdateDto) => new Order()
    {
        Id = orderUpdateDto.Id,
        OrderDate = orderUpdateDto.OrderDate,
        CustomerId = orderUpdateDto.CustomerId,
        ProductId = orderUpdateDto.ProductId,
        PurchaseQuantity = orderUpdateDto.PurchaseQuantity
    };
    
    public static OrderDto FromEntity(Order order) => new OrderDto(
        order.Id,
        order.ProductId,
        order.CustomerId,
        order.PurchaseQuantity,
        order.OrderDate
    );
    
    public static IEnumerable<OrderDto> FromEntities(IEnumerable<Order> orders) => orders.Select(FromEntity);
    
    
}