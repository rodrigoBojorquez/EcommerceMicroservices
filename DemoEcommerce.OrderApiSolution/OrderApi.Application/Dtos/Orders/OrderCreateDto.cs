using System.ComponentModel.DataAnnotations;

namespace OrderApi.Application.Dtos.Orders;

public record OrderCreateDto(
    [Required] Guid ProductId,
    [Required] Guid CustomerId,
    [Required] int PurchaseQuantity);