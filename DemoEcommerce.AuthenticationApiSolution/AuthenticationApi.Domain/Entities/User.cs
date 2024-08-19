namespace AuthenticationApi.Domain.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Address { get; set; }
    public required string Role { get; set; }
    public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}