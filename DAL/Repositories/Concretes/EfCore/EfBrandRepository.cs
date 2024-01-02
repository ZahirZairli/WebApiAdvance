using WebApiAdvance.Core.DAL.Concretes.EfCore;
using WebApiAdvance.DAL.EfCore;
using WebApiAdvance.DAL.Repositories.Abstracts;
using WebApiAdvance.Entities;

namespace WebApiAdvance.DAL.Repositories.Concretes.EfCore;

public class EfBrandRepository : EfBaseRepository<Brand, AppDbContext>,IBrandRepository
{
    public EfBrandRepository(AppDbContext context) : base(context)
    {
    }
}
