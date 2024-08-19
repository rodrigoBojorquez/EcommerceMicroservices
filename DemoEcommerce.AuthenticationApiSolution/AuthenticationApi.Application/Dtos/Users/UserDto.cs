namespace AuthenticationApi.Application.Dtos.Users;

public record UserDto(
    Guid Id,
    string Name,
    string PhoneNumber,
    string Email,
    string Address,
    string Role);