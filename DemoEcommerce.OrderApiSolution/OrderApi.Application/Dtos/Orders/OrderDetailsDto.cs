namespace OrderApi.Application.Dtos.Orders;

public record OrderDetailsDto(
    Guid OrderId,
    Guid ProductId,
    Guid CustomerId,
    string Email,
    string PhoneNumber,
    string ProductName,
    int PurchaseQuantity,
    decimal UnitPrice,
    decimal TotalPrice,
    string Address,
    DateTime OrderDate);