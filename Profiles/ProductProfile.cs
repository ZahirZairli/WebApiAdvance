using AutoMapper;
using WebApiAdvance.Entities;
using WebApiAdvance.Entities.Dtos.Products;

namespace WebApiAdvance.Profiles;

public class ProductProfile:Profile
{
    public ProductProfile()
    {
        CreateMap<Product, GetProductDto>()
                        .ForMember(p => p.BrandName, opt => opt.MapFrom(p => p.Brand.Name));
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();

        ////If property names are different 
        //CreateMap<Product, GetProductDto>()
        //         .ForMember(p => p.Name, opt => opt.MapFrom(p => p.Name));
    }
}
