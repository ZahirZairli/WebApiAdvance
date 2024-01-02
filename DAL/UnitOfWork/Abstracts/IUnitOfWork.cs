using WebApiAdvance.DAL.Repositories.Abstracts;

namespace WebApiAdvance.DAL.UnitOfWork.Abstracts;

public interface IUnitOfWork
{
    public IProductRepository ProductRepository { get;}
    public IBrandRepository BrandRepository { get; }
    Task SaveChangeAsync();
}
