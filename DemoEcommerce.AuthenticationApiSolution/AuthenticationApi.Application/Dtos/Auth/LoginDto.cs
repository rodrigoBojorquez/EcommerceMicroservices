using System.ComponentModel.DataAnnotations;

namespace AuthenticationApi.Application.Dtos.Auth;

public record LoginDto(
    [Required] string Email,
    [Required, MinLength(6)] string Password);