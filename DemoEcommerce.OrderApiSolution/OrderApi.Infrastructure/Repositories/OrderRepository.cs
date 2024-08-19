using System.Linq.Expressions;
using Ecommerce.SharedLibrary.Logs;
using Ecommerce.SharedLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using OrderApi.Application.Interfaces;
using OrderApi.Domain.Entities;
using OrderApi.Infrastructure.Data;

namespace OrderApi.Infrastructure.Repositories;

public class OrderRepository : IOrder
{
    private readonly OrderDbContext _context;

    public OrderRepository(OrderDbContext context)
    {
        _context = context;
    }

    public async Task<Response> CreateAsync(Order entity)
    {
        try
        {
            var order = _context.Orders.Add(entity).Entity;
            await _context.SaveChangesAsync();
            
            return order.Id != Guid.Empty
                ? new Response(true, "Order created successfully")
                : new Response(false, "An error occurred while creating the order");
        }
        catch (Exception ex)
        {
            LogException.LogExceptions(ex);
            return new Response(false, "An error occurred while creating the order");
        }
    }

    public async Task<Response> UpdateAsync(Order entity)
    {
        try
        {
            var order = await FindByIdAsync(entity.Id);
            if (order is null)
                return new Response(false, "Order not found");

            _context.Entry(order).State = EntityState.Detached;
            _context.Orders.Update(entity); 
            await _context.SaveChangesAsync();
            return new Response(true, "Order updated successfully");
        }
        catch (Exception ex)
        {
            LogException.LogExceptions(ex);
            return new Response(false, "An error occurred while creating the order");
        }
    }

    public async Task<Response> DeleteAsync(Guid id)
    {
        try
        {
            var order = await FindByIdAsync(id);
            if (order is null)
                return new Response(false, "Order not found");
            
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            
            return new Response(true, "Order deleted successfully");
        }
        catch (Exception ex)
        {
            LogException.LogExceptions(ex);
            return new Response(false, "An error occurred while deleting the order");
        }
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        try
        {
            var orders = await _context.Orders.AsNoTracking().ToListAsync();
            return orders is not null ? orders : null!;
        }
        catch (Exception ex)
        {
            LogException.LogExceptions(ex);
            throw new Exception("An error occurred while getting the orders");
        }
    }

    public async Task<Order> FindByIdAsync(Guid id)
    {
        try
        {
            var order = await _context.Orders.FindAsync(id);
            return order is not null ? order : null!;
        }
        catch (Exception ex)
        {
            LogException.LogExceptions(ex);
            throw new Exception("An error occurred while finding the order");
        }
    }

    public async Task<Order> GetByAsync(Expression<Func<Order, bool>> expression)
    {
        try
        {
            var order = await _context.Orders.Where(expression).FirstOrDefaultAsync();
            return order is not null ? order : null!;
        }
        catch (Exception ex)
        {
            LogException.LogExceptions(ex);
            throw new Exception("An error occurred while finding the order");
        }
    }

    public async Task<IEnumerable<Order>> GetOrdersAsync(Expression<Func<Order, bool>> expression)
    {
        try
        {
            var orders = await _context.Orders.Where(expression).AsNoTracking().ToListAsync();
            return orders is not null ? orders.AsEnumerable() : null!;
        }
        catch (Exception ex)
        {
            LogException.LogExceptions(ex);
            throw new Exception("An error occurred while creating the order");
        }
    }
}