using AutoMapper;
using WebApiAdvance.Entities;
using WebApiAdvance.Entities.Dtos.Authentication;

namespace WebApiAdvance.Profiles;

public class AuthenticationProfile:Profile
{
    public AuthenticationProfile()
    {
        //CreateMap<RegisterDto, AppUser>().ReverseMap(); //Reverse Map eksi ucun de istifade edilir
        CreateMap<RegisterDto, AppUser>();
        CreateMap<LoginDto, AppUser>();
    }
}
