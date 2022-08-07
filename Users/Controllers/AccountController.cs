using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Listonic.Domain.Services.Communication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Users.Resources;

namespace Users.Controllers;

[Route("/api/[controller]")]
public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public AccountController(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration,
        IMapper mapper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IdentityResult> Register([FromBody] UserCredentialsResource userCredentials)
    {
        var user = _mapper.Map<UserCredentialsResource, IdentityUser>(userCredentials);
        user.SecurityStamp = Guid.NewGuid().ToString();
        return await _userManager.CreateAsync(user, userCredentials.Password);
    }

    [HttpPost("auth")]
    public async Task<IActionResult> Auth([FromBody] UserCredentialsResource userCredentials)
    {
        var user = await _userManager.FindByNameAsync(userCredentials.UserName);
        
        if (user != null && await _userManager.CheckPasswordAsync(user, userCredentials.Password))  
        {  
            // var userRoles = await _userManager.GetRolesAsync(user);  
  
            var authClaims = new List<Claim>  
            {  
                new Claim(ClaimTypes.NameIdentifier, user.Id),  
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),  
            };  
  
            // foreach (var userRole in userRoles)  
            // {  
            //     authClaims.Add(new Claim(ClaimTypes.Role, userRole));  
            // }  
  
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));  
  
            var token = new JwtSecurityToken(  
                issuer: _configuration["JWT:ValidIssuer"],  
                audience: _configuration["JWT:ValidAudience"],  
                expires: DateTime.Now.AddHours(3),  
                claims: authClaims,  
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)  
            );  
  
            return Ok(new  
            {  
                token = new JwtSecurityTokenHandler().WriteToken(token),  
                expiration = token.ValidTo  
            });  
        }  
        return Unauthorized();
    }
}