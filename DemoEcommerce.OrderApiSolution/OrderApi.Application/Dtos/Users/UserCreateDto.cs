using System.ComponentModel.DataAnnotations;

namespace OrderApi.Application.Dtos.Users;

public record UserCreateDto(
    [Required] string Name,
    [Required, EmailAddress] string Email,
    [Required] string PhoneNumber,
    [Required] string Address,
    [Required] string Role);