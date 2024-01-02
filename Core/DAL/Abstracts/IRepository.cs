using System.Linq.Expressions;
using WebApiAdvance.Entities;

namespace WebApiAdvance.Core.DAL.Abstracts;

public interface IRepository<T> where T: class,new()
{
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, params string[] includes);
    Task<List<T>> GetAllPaginatedAsync(int size, int page, Expression<Func<T, bool>> filter = null, params string[] includes);
    Task<T> GetAsync(Expression<Func<T, bool>> filter, params string[] includes);
    Task<bool> ExistAsync(Expression<Func<T, bool>> filter);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}
