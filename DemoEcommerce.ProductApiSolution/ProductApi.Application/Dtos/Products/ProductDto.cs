using System.ComponentModel.DataAnnotations;

namespace ProductApi.Application.Dtos.Products;

public record ProductDto(
    Guid Id,
    string Name,
    int Quantity,
    decimal Price
    );