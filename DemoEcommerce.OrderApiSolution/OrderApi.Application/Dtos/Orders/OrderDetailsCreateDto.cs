using System.ComponentModel.DataAnnotations;

namespace OrderApi.Application.Dtos.Orders;

public record OrderDetailsCreateDto(
    [Required] Guid OrderId,
    [Required] Guid ProductId,
    [Required] Guid CustomerId,
    [Required, EmailAddress] string Email,
    [Required] string PhoneNumber,
    [Required] string ProductName,
    [Required] int PurchaseQuantity,
    [Required, DataType(DataType.Currency)] decimal UnitPrice,
    [Required, DataType(DataType.Currency)] decimal TotalPrice,
    [Required] string Address,
    [Required] DateTime OrderDate);