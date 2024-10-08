using System.Linq.Expressions;
using Ecommerce.SharedLibrary.Responses;

namespace Ecommerce.SharedLibrary.Interface;

public interface IGenericInterface<T> where T : class
{
    Task<Response> CreateAsync(T entity);
    
    Task<Response> UpdateAsync(T entity);
    
    Task<Response> DeleteAsync(Guid id);
    
    Task<IEnumerable<T>> GetAllAsync();
    
    Task<T> FindByIdAsync(Guid id);
    
    Task<T> GetByAsync(Expression<Func<T, bool>> predicate);
}