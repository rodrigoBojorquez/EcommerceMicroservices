using AuthenticationApi.Application.Dtos.Auth;
using AuthenticationApi.Domain.Entities;
using Ecommerce.SharedLibrary.Interface;
using Ecommerce.SharedLibrary.Responses;

namespace AuthenticationApi.Application.Interfaces;

public interface IUserRepository : IGenericInterface<User>
{
    Task<Response> RegisterAsync(RegisterDto registerDto);
    Task<Response> LoginAsync(LoginDto loginDto);
}