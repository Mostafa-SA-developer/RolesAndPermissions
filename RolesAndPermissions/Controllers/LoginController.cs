using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RolesAndPermissions.DbC;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RolesAndPermissions.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : Controller
    {
        DbDataContext dbDataContext;

        public LoginController(DbDataContext dbDataContext)
        {
            this.dbDataContext = dbDataContext;
        }

        [HttpPost] 
        public IActionResult Login(LoginUser loginUser)
        {

            var find = dbDataContext.Users.Where(x => x.Email == loginUser.Email && x.Password ==loginUser.Password).FirstOrDefault();
            if (find != null)
            {
                var builder = WebApplication.CreateBuilder();

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(builder.Configuration["JWTSettings:Key"]); // Same key as in the configuration
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Audience = builder.Configuration["JWTSettings:Audience"],
                    Issuer = builder.Configuration["JWTSettings:Issuer"],
                    Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, loginUser.Email) }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);
                return Ok(tokenString);
            }
            else return NotFound();
           
        }
    }



}


public class LoginUser
{
    public string Email { get; set; }
    public string Password { get; set; }
}