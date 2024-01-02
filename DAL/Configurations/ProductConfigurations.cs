using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data;
using WebApiAdvance.Entities;

namespace WebApiAdvance.DAL.Configurations;

public class ProductConfigurations : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.Name)
               .IsRequired()
               .HasColumnType(SqlDbType.NVarChar.ToString())
               .HasMaxLength(150);

        //builder.HasOne(p => p.Brand) //Productin icindeki 
        //        .WithMany(b => b.Products) //Brandin icindeki
        //        .HasForeignKey(p => p.BrandId);
    }
}
