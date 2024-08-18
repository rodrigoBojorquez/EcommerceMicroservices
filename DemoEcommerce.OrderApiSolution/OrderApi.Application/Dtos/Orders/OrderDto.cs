namespace OrderApi.Application.Dtos.Orders;

public record OrderDto(
    Guid Id, 
    Guid ProductId,
    Guid UserId,
    int PurchaseQuantity,
    DateTime OrderDate);