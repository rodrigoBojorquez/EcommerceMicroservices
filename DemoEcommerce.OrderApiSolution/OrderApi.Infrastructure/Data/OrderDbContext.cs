using Microsoft.EntityFrameworkCore;
using OrderApi.Domain.Entities;

namespace OrderApi.Infrastructure.Data;

public class OrderDbContext: DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> opt) : base(opt)
    {
    }
    
    public DbSet<Order> Orders { get; set; }
}