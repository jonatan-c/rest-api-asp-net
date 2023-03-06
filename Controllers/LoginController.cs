
using  Microsoft.AspNetCore.Mvc;
 using  Microsoft.EntityFrameworkCore;
 using  System.Collections.Generic;
 using  System.Threading.Tasks;
 using  web_api.Data;
 using  web_api.Data.DataBankModels;
 using  web_api.Dtos;
 using  web_api.Services;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


namespace web_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly LoginService _loginService;
    private IConfiguration _config;

    public LoginController(LoginService loginService, IConfiguration config)
    {
        this._loginService = loginService;
        this._config = config;
    }

    [HttpPost("authenticate")]
    public async Task<ActionResult?> Login(AdminDto adminDto)
    {
        var admin = await _loginService.GetAdmin(adminDto);

        if (admin == null)
        {
            return BadRequest(new { message = "Credenciales invalidas." });
        }

        string jwtToken = GenerateToken(admin);

        // generar un token 
        return Ok(new { token = jwtToken });
    }

    private string GenerateToken(Administrator admin)
    {
        var claims = new[]
            {
                new Claim(ClaimTypes.Name, admin.Name),
                new Claim(ClaimTypes.Email, admin.Email),
                new Claim("AdminType", admin.AdminType)
            };
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JWT:Key").Value));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var securityToken = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: creds
        ); 

        string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

        return token;
    }
}