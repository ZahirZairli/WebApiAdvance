using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection;
using WebApiAdvance.Entities;

namespace WebApiAdvance.DAL.EfCore;

public class AppDbContext:IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options){}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Product>()
        //            .Property(p => p.Name)
        //            .IsRequired()
        //            .HasColumnType(SqlDbType.NVarChar.ToString())
        //            .HasMaxLength(100);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
    public DbSet<Product> Products { get; set; }
    public DbSet<Brand> Brands { get; set; }
}
