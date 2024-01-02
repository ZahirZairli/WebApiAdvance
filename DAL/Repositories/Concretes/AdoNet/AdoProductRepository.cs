using System.Linq.Expressions;
using WebApiAdvance.DAL.Repositories.Abstracts;
using WebApiAdvance.Entities;

namespace WebApiAdvance.DAL.Repositories.Concretes.AdoNet;

public class AdoProductRepository : IProductRepository
{
    public Task AddAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public void Delete(Product product)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistAsync(Expression<Func<Product, bool>> filter)
    {
        throw new NotImplementedException();
    }

    public Task<List<Product>> GetAllAsync(Expression<Func<Product, bool>> filter = null, params string[] includes)
    {
        throw new NotImplementedException();
    }

    public Task<List<Product>> GetAllPaginatedAsync(int size, int page, Expression<Func<Product, bool>> filter = null, params string[] includes)
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetAsync(Expression<Func<Product, bool>> filter, params string[] includes)
    {
        throw new NotImplementedException();
    }

    public Task SaveChangeAsync()
    {
        throw new NotImplementedException();
    }

    public void Update(Product product)
    {
        throw new NotImplementedException();
    }
}
