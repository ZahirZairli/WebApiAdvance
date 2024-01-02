using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using WebApiAdvance.DAL.EfCore;
using WebApiAdvance.Entities.Dtos.Authentication;
using WebApiAdvance.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebApiAdvance;

public static class ConfigurationService
{
    public static IServiceCollection AddApiConfiguration(this IServiceCollection services,IConfiguration configuration)
    {
        TokenOption tokenOption = configuration.GetSection("TokenOptions").Get<TokenOption>();
        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("Default"));
        });
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddIdentity<AppUser, IdentityRole>()
                        .AddEntityFrameworkStores<AppDbContext>()
                        .AddDefaultTokenProviders();
        //for Using our JwttokenProvide we must write and install JWT Bearer start
        services.AddAuthentication(opt =>
        {
            opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = tokenOption.Issuer,
                ValidAudience = tokenOption.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOption.SecurityKey))
            };
        });
        //for Using our JwttokenProvide  end
        return services;
    }
}
