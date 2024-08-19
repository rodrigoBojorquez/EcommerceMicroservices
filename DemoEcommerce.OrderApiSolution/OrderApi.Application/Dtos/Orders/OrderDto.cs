namespace OrderApi.Application.Dtos.Orders;

public record OrderDto(
    Guid Id, 
    Guid ProductId,
    Guid CustomerId,
    int PurchaseQuantity,
    DateTime OrderDate);