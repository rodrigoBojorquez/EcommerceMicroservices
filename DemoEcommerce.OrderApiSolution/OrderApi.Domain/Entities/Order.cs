namespace OrderApi.Domain.Entities;

public class Order
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid CustomerId { get; set; }
    public int PurchaseQuantity { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

}