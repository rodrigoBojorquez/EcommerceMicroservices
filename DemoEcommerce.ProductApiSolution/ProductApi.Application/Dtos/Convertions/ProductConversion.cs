using ProductApi.Application.Dtos.Products;
using ProductApi.Domain.Entities;

namespace ProductApi.Application.Dtos.Convertions;

public class ProductConversion
{
    public static Product ToEntity(ProductDto productDto) =>
        new Product
        {
            Id = productDto.Id,
            Name = productDto.Name,
            Price = productDto.Price,
            Quantity = productDto.Quantity
        };

    public static Product ToEntity(ProductCreateDto productDto) =>
        new Product
        {
            Id = Guid.NewGuid(),
            Name = productDto.Name,
            Price = productDto.Price,
            Quantity = productDto.Quantity
        };

    public static Product ToEntity(ProductUpdateDto productDto) =>
        new Product
        {
            Id = productDto.Id,
            Name = productDto.Name,
            Price = productDto.Price,
            Quantity = productDto.Quantity
        };

    public static ProductDto FromEntity(Product product) =>
        new ProductDto(product.Id, product.Name!, product.Quantity, product.Price);

    public static IEnumerable<ProductDto> FromEntities(IEnumerable<Product> products) =>
        products.Select(p => new ProductDto(p.Id, p.Name!, p.Quantity, p.Price));
}