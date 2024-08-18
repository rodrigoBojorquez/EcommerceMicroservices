using System.Linq.Expressions;
using Ecommerce.SharedLibrary.Logs;
using Ecommerce.SharedLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ProductApi.Application.Interfaces;
using ProductApi.Domain.Entities;
using ProductApi.Infrastructure.Data;

namespace ProductApi.Infrastructure.Repositories;

public class ProductRepository : IProduct
{
    private readonly ProductDbContext _context;

    public ProductRepository(ProductDbContext context)
    {
        _context = context;
    }

    public async Task<Response> CreateAsync(Product entity)
    {
        try
        {
            var getProduct = await GetByAsync(_ => _.Name!.Equals(entity.Name));

            if (getProduct is not null && !string.IsNullOrEmpty(getProduct.Name))
                return new Response(false, $"{entity.Name} already exists");

            var currentEntity = _context.Products.Add(entity).Entity;
            await _context.SaveChangesAsync();

            if (currentEntity is not null && currentEntity.Id != Guid.Empty)
                return new Response(true, $"{entity.Name} created successfully");
            else
                return new Response(false, "Error creating product");
        }
        catch (Exception ex)
        {
            LogException.LogExceptions(ex);
            return new Response(false, "Error creating product");
        }
    }

    public async Task<Response> UpdateAsync(Product entity)
    {
        try
        {
            var product = await FindByIdAsync(entity.Id);
            
            if (product is null)
                return new Response(false, "Product not found");

            _context.Entry(product).State = EntityState.Detached;
            _context.Products.Update(entity);
            await _context.SaveChangesAsync();
            
            return new Response(true, "Product updated successfully");
        }
        catch (Exception ex)
        {
            LogException.LogExceptions(ex);
            return new Response(false, "Error updating product");
        }
    }

    public async Task<Response> DeleteAsync(Guid id)
    {
        try
        {
            var product = await FindByIdAsync(id);

            if (product is null)
                return new Response(false, "Product not found");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return new Response(true, "Product deleted successfully");
        }
        catch (Exception ex)
        {
            LogException.LogExceptions(ex);
            return new Response(false, "Error deleting product");
        }
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        try
        {
            var products = await _context.Products.AsNoTracking().ToListAsync();
            return products ?? null;
        }
        catch (Exception ex)
        {
            LogException.LogExceptions(ex);
            throw new Exception("Error getting products");
        }
    }

    public async Task<Product> FindByIdAsync(Guid id)
    {
        try
        {
            var product = await _context.Products.FindAsync(id);
            return product is not null ? product : null!;
        }
        catch (Exception ex)
        {
            LogException.LogExceptions(ex);
            throw new Exception("Error finding product");
        }
    }

    public async Task<Product> GetByAsync(Expression<Func<Product, bool>> predicate)
    {
        try
        {
            var product = await _context.Products.Where(predicate).FirstOrDefaultAsync();
            return product is not null ? product : null!;
        }
        catch (Exception ex)
        {
            LogException.LogExceptions(ex);
            throw new Exception("Error getting product");
        }
    }
}