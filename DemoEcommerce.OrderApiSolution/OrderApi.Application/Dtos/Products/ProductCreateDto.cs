using System.ComponentModel.DataAnnotations;

namespace OrderApi.Application.Dtos.Products;

public record ProductCreateDto(
    [Required] string Name,
    [Required] int Quantity,
    [Required, DataType(DataType.Currency)] decimal Price);