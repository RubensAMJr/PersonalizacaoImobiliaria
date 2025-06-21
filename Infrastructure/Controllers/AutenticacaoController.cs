using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PersonalizacaoImobiliaria.Domain.Enums;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PersonalizacaoImobiliaria.Infrastructure.Controllers;

[ApiController]
[Route("auth")]
public class AutenticacaoController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AutenticacaoController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult GenerateToken(EPerfilUsuario perfil,string userName)
    {
        var infoUsuario = new[]
        {
            new Claim(ClaimTypes.Role, perfil.ToString()),
            new Claim("IdUsuario", Guid.NewGuid().ToString()),
            new Claim("NomeUsuario", userName)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: infoUsuario,
            expires: DateTime.UtcNow.AddMinutes(60),
            signingCredentials: credenciais);

        var AuthKey = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(AuthKey);
    }
}   
