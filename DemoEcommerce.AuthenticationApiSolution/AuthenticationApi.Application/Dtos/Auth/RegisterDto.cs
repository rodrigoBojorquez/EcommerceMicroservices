using System.ComponentModel.DataAnnotations;

namespace AuthenticationApi.Application.Dtos.Auth;

public record RegisterDto(
    [Required] string Name,
    [Required] string PhoneNumber,
    [Required] string Email,
    [Required] string Password,
    [Required] string Address);