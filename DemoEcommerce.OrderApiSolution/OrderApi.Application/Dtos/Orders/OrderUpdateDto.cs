using System.ComponentModel.DataAnnotations;

namespace OrderApi.Application.Dtos.Orders;

public record OrderUpdateDto(
    [Required] Guid Id,
    [Required] Guid ProductId,
    [Required] Guid UserId,
    [Required] int PurchaseQuantity);