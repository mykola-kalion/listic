using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Users.Resources;

namespace Users.Controllers;

[Route("/api/[controller]")]
public class AccountController : Controller
{
    private readonly UserManager<TelegramUser> _userManager;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public AccountController(
        UserManager<TelegramUser> userManager,
        IConfiguration configuration,
        IMapper mapper)
    {
        _userManager = userManager;
        _configuration = configuration;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IdentityResult> Register([FromBody] UserCredentialsResource userCredentials)
    {
        var user = _mapper.Map<UserCredentialsResource, TelegramUser>(userCredentials);
        user.SecurityStamp = Guid.NewGuid().ToString();
        return await _userManager.CreateAsync(user, userCredentials.Password);
    }

    [HttpPost("auth")]
    public async Task<IActionResult> Auth([FromBody] UserCredentialsResource userCredentials)
    {
        var user = await _userManager.FindByNameAsync(userCredentials.UserName);
        var roles = await _userManager.GetRolesAsync(user);

        if (user == null || !await _userManager.CheckPasswordAsync(user, userCredentials.Password))
            return Unauthorized();
        
        var authClaims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, user.Id),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        
        authClaims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

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
}