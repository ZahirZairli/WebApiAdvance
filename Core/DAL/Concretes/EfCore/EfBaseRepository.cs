using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApiAdvance.Core.DAL.Abstracts;
using WebApiAdvance.Entities;

namespace WebApiAdvance.Core.DAL.Concretes.EfCore;

public abstract class EfBaseRepository<TEntity, TContext> : IRepository<TEntity>
    where TEntity : class, new()
    where TContext : DbContext
{
    private readonly TContext _context;
    private readonly DbSet<TEntity> _dbSet; 

    public EfBaseRepository(TContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> filter)
    {
        return await _dbSet.AnyAsync(filter);
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, params string[] includes)
    {
        IQueryable<TEntity> query = GetQuery(includes);
        return await query.Where(filter).FirstOrDefaultAsync();
    }
    public Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, params string[] includes)
    {
        IQueryable<TEntity> query = GetQuery(includes);
        return filter == null
                        ? query.ToListAsync()
                        : query.Where(filter).ToListAsync();
    }

    public Task<List<TEntity>> GetAllPaginatedAsync(int size, int page, Expression<Func<TEntity, bool>> filter = null, params string[] includes)
    {
        IQueryable<TEntity> query = GetQuery(includes);
        return filter == null
                        ? query.Skip((page - 1) * size).Take(size).ToListAsync()
                        : query.Where(filter).Skip((page - 1) * size).Take(size).ToListAsync();
    }

    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }
    private IQueryable<TEntity> GetQuery(string[] includes)
    {
        IQueryable<TEntity> query = _dbSet;
        foreach (string item in includes)
        {
            query = query.Include(item);
        }
        return query;
    }
}
