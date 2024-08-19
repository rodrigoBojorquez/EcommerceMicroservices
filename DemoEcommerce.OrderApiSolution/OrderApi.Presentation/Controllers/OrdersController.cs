using Ecommerce.SharedLibrary.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Application.Conversions;
using OrderApi.Application.Dtos.Orders;
using OrderApi.Application.Interfaces;

namespace OrderApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrder _orderRepository;
        private readonly IOrderService _orderService;
        
        public OrdersController(IOrder orderRepository, IOrderService orderService)
        {
            _orderRepository = orderRepository;
            _orderService = orderService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            var orders = await _orderRepository.GetAllAsync();

            if (!orders.Any())
                return NotFound("No orders found");

            var list = OrderConversion.FromEntities(orders);
            
            return Ok(list);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(Guid id)
        {
            var order = await _orderRepository.FindByIdAsync(id);

            if (order is null)
                return NotFound($"Order not found");
            
            var response = OrderConversion.FromEntity(order);
            
            return Ok(response);
        }
        
        [HttpPost]
        public async Task<ActionResult<Response>> CreateOrder([FromBody] OrderCreateDto newOrder)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var order = OrderConversion.ToEntity(newOrder);
            var response = await _orderRepository.CreateAsync(order);

            return response.Flag ? Ok(response) : BadRequest(response);
        }
        
        [HttpPut]
        public async Task<ActionResult<Response>> UpdateOrder([FromBody] OrderUpdateDto order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var entity = OrderConversion.ToEntity(order);
            var response = await _orderRepository.UpdateAsync(entity);

            return response.Flag ? Ok(response) : BadRequest(response);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> DeleteOrder(Guid id)
        {
            var response = await _orderRepository.DeleteAsync(id);
            return response.Flag ? Ok(response) : BadRequest(response);
        }
        
        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByCustomerId(Guid customerId)
        {
            var orders = await _orderService.GetOrdersByCustomerIdAsync(customerId);
            
            return orders.Any() ? Ok(orders) : NotFound("No orders found");
        }
        
        [HttpGet("details/{orderId}")]
        public async Task<ActionResult<OrderDetailsDto>> GetOrderDetails(Guid orderId)
        {
            var order = await _orderService.GetOrderDetails(orderId);
            
            return order is not null ? Ok(order) : NotFound("Order not found");
        }
    }
}
