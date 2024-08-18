using System.ComponentModel.DataAnnotations;

namespace ProductApi.Application.Dtos.Products;

public record ProductCreateDto(
    [Required] string Name,
    [Required, Range(1, int.MaxValue)] int Quantity,
    [Required, DataType(DataType.Currency)] decimal Price);