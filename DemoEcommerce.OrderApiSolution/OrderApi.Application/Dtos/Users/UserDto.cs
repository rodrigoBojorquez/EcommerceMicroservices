namespace OrderApi.Application.Dtos.Users;

public record UserDto(
    Guid Id,
    string Name,
    string Email,
    string PhoneNumber,
    string Address,
    string Role);