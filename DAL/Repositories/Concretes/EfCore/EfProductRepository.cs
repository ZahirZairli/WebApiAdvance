using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApiAdvance.Core.DAL.Concretes.EfCore;
using WebApiAdvance.DAL.EfCore;
using WebApiAdvance.DAL.Repositories.Abstracts;
using WebApiAdvance.Entities;

namespace WebApiAdvance.DAL.Repositories.Concretes.EfCore;

public class EfProductRepository : EfBaseRepository<Product, AppDbContext>, IProductRepository
{
    public EfProductRepository(AppDbContext context) : base(context)
    {
    }
}
