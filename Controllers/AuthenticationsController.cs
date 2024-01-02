using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiAdvance.Entities;
using WebApiAdvance.Entities.Dtos.Authentication;

namespace WebApiAdvance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationsController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly TokenOption _tokenOption;
        public AuthenticationsController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _configuration = configuration;
            _tokenOption = _configuration.GetSection("TokenOptions").Get<TokenOption>();
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            AppUser appUser = _mapper.Map<AppUser>(registerDto);
            IdentityResult identityResult = await _userManager.CreateAsync(appUser, registerDto.Password);
            if (!identityResult.Succeeded)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Errors = identityResult.Errors
                });
            }
            var result = await _userManager.AddToRoleAsync(appUser, Roles.User.ToString());
            if (!result.Succeeded)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Errors = identityResult.Errors
                });
            }
            return Ok(new
            {
                Message = "User was created succesfully!"
            });
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            AppUser user = await _userManager.FindByNameAsync(loginDto.Username);
            if (user is null) return NotFound();
            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password)) return Unauthorized();

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOption.SecurityKey));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            JwtHeader header = new JwtHeader(signingCredentials);

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim("Fullname",user.Fullname)
            };
            IList<string> roles = await _userManager.GetRolesAsync(user);
            foreach (string role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            JwtPayload payload = new JwtPayload(
                issuer: _tokenOption.Issuer,
                audience: _tokenOption.Audience,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(_tokenOption.AccessTokenExpiration),
                claims: claims
                );
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(header, payload);
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            string token = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);
            return Ok(new
            {
                token=token,
                expires = DateTime.UtcNow.AddMinutes(_tokenOption.AccessTokenExpiration)
            });
        }
        enum Roles
        {
            Admin,
            User
        }
    }
}
