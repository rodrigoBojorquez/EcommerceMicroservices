using System.Linq.Expressions;
using AuthenticationApi.Application.Dtos.Auth;
using AuthenticationApi.Application.Interfaces;
using AuthenticationApi.Domain.Entities;
using AuthenticationApi.Infrastructure.Data;
using Ecommerce.SharedLibrary.Responses;

namespace AuthenticationApi.Infrastructure.Repositories;

public class UserRepository: IUserRepository
{
    private readonly AuthenticationDbContext _context;
    
    public UserRepository(AuthenticationDbContext context)
    {
        _context = context;
    }
    
    public Task<Response> CreateAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public Task<Response> UpdateAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public Task<Response> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User> FindByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByAsync(Expression<Func<User, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<Response> RegisterAsync(RegisterDto registerDto)
    {
        throw new NotImplementedException();
    }

    public Task<Response> LoginAsync(LoginDto loginDto)
    {
        throw new NotImplementedException();
    }
}