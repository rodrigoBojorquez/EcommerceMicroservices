using System.Net.Http.Json;
using OrderApi.Application.Conversions;
using OrderApi.Application.Dtos.Orders;
using OrderApi.Application.Dtos.Products;
using OrderApi.Application.Dtos.Users;
using OrderApi.Application.Interfaces;
using Polly;
using Polly.Registry;

namespace OrderApi.Application.Services;

public class OrderService : IOrderService
{
    private readonly HttpClient _httpClient;
    private readonly ResiliencePipelineProvider<string> _resiliencePipelineProvider;
    private readonly IOrder _orderRepository;

    public OrderService(HttpClient httpClient,
        ResiliencePipelineProvider<string> resiliencePipelineProvider,
        IOrder orderRepository)
    {
        _httpClient = httpClient;
        _resiliencePipelineProvider = resiliencePipelineProvider;
        _orderRepository = orderRepository;
    }

    /*
     * Llamar a ProductApi usando httpclient
     * Redireccionar al ApiGateway porque ProductApi no es accesible desde fuera
     */
    public async Task<ProductDto> GetProduct(Guid productId)
    {
        var productResponse = await _httpClient.GetAsync($"/api/products/{productId}");

        if (!productResponse.IsSuccessStatusCode)
            return null!;

        var product = await productResponse.Content.ReadFromJsonAsync<ProductDto>();
        return product!;
    }

    /*
     * Llamar a UserApi usando httpclient
     * Redireccionar al ApiGateway porque UserApi no es accesible desde fuera
     */
    public async Task<UserDto> GetUser(Guid userId)
    {
        // CAMBIAR POR USERS
        var userResponse = await _httpClient.GetAsync($"/api/products/{userId}");

        if (!userResponse.IsSuccessStatusCode)
            return null!;

        var user = await userResponse.Content.ReadFromJsonAsync<UserDto>();
        return user!;
    }

    public async Task<IEnumerable<OrderDto>> GetOrdersByCustomerIdAsync(Guid customerId)
    {
        var orders = await _orderRepository.GetOrdersAsync(order => order.CustomerId == customerId);
        if (!orders.Any())
            return null!;

        var dtos = OrderConversion.FromEntities(orders);
        return dtos;
    }

    public async Task<OrderDetailsDto> GetOrderDetails(Guid orderId)
    {
        var order = await _orderRepository.FindByIdAsync(orderId);
        if (order is null)
            return null!;

        var retryPipeline = _resiliencePipelineProvider.GetPipeline("my-retry-pipeline");

        if (retryPipeline is null)
            throw new InvalidOperationException("Retry pipeline not found");
        
        var productDto = await retryPipeline.ExecuteAsync(async token => await GetProduct(order.ProductId));
        var userDto = await retryPipeline.ExecuteAsync(async token => await GetUser(order.CustomerId));

        return new OrderDetailsDto(
            order.Id,
            productDto.Id,
            userDto.Id,
            userDto.Email,
            userDto.PhoneNumber,
            productDto.Name,
            order.PurchaseQuantity,
            productDto.Price,
            productDto.Price * order.PurchaseQuantity,
            userDto.Address,
            order.OrderDate
        );
    }
}