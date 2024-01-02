using Microsoft.EntityFrameworkCore;
using WebApiAdvance.DAL.EfCore;
using WebApiAdvance.DAL.Repositories.Abstracts;
using WebApiAdvance.DAL.Repositories.Concretes.EfCore;
using WebApiAdvance.DAL.UnitOfWork.Abstracts;

namespace WebApiAdvance.DAL.UnitOfWork.Concretes;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IProductRepository _productRepository;
    private IBrandRepository _brandRepository;
    public IProductRepository ProductRepository => _productRepository=_productRepository ?? new EfProductRepository(_context);

    public IBrandRepository BrandRepository => _brandRepository =_brandRepository ?? new EfBrandRepository(_context);

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }
    public async Task SaveChangeAsync()
    {
        await _context.SaveChangesAsync();
    }
}
