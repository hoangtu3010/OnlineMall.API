using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineMall.API.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMall.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthManagermentController : ControllerBase
    {
        private readonly SystemDbContext _context;
        public IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthManagermentController(SystemDbContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public static string GetMD5(string str)
        {
            MD5 md5 =  new MD5CryptoServiceProvider();
            byte[] frData = Encoding.UTF8.GetBytes(str);
            byte[] toData = md5.ComputeHash(frData);
            string hashString = "";
            for (int i = 0; i < toData.Length; i++)
            {
                hashString += toData[i].ToString("x2");
            }
            return hashString;
        }

        [HttpPost]
        public async Task<ActionResult> Register([FromBody] User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> Login([FromBody] User _user)
        {
            if(_user != null && _user.Email != null && _user.Password != null)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == _user.Email && u.Password == _user.Password);
                if(user != null)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
                        new Claim("Id", user.Id.ToString()),
                        new Claim("Name", user.Name),
                        new Claim("Email", user.Email),
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    
                    var sign = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.Now.AddDays(1),
                        signingCredentials: sign
                    );

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                return BadRequest("Email or Password invalid!");
            }
            return BadRequest();
        }

        [HttpGet]
        public IActionResult GetCurrentUser()
        {
            try
            {
                var user = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                return Ok(user);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
