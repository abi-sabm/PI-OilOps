using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OilOps.DataAccess;
using OilOps.Models;

namespace OilOps.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly string secretKey;
    private readonly OilOpsDbContext _dbContext;
    IConfiguration configuration;
    
    public LoginController(IConfiguration configuration, OilOpsDbContext dbContext)
    {
        this.configuration = configuration;
        secretKey = configuration.GetSection("Jwt").GetSection("key").ToString();
        _dbContext = dbContext;
    } 
    
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginUser request)
    {
        //IActionResult response = Unauthorized();
        var user = await _dbContext.LoginUsers.FindAsync(request.UserName);
        if (user == null)
        {
            return StatusCode(StatusCodes.Status401Unauthorized, new { token = "Inexistente / Inhabilitado" });
        }
        if (request.UserName == user.UserName && request.Password == user.Password)
        {
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature
            );

            var subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.UserName),
            });

            var expires = DateTime.UtcNow.AddMinutes(10);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = expires,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            return StatusCode(StatusCodes.Status200OK, new { token = jwtToken });
        }
        else
        {
            return StatusCode(StatusCodes.Status401Unauthorized, new { token = "Inexistente / Inhabilitado" });
        }
    }
}