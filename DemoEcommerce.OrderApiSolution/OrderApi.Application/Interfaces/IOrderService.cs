using OrderApi.Application.Dtos.Orders;

namespace OrderApi.Application.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetOrdersByCustomerIdAsync(Guid customerId);
    Task<OrderDetailsDto> GetOrderDetails(Guid orderId);
}