using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using teemUpAPIv2.Models;
using teemUpAPIv2.Data;
using teemUpAPIv2.Services.UserServices;

namespace teemUpAPIv2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();

        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly DataContext _context;
        

        public AuthController(IConfiguration configuration, IUserService userService, DataContext context)
        {
            _configuration = configuration;
            _userService = userService;
            _context = context;
            
        }
        
        [HttpGet, Authorize]
        public ActionResult<string> GetMe()
        {
            var email = _userService.GetMyName();
            return Ok(email);
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            

            

            var userToCreate = new User
            {
                email = request.email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            _context.Users.Add(userToCreate);
            await _context.SaveChangesAsync();

            

            return Ok(user.email);
            

        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            user = await _context.Users.FindAsync(request.email);
            
   
            if (user == null)
            {
                return NotFound("User not found");
            }
            
            if(!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong password.");
            }

            string token = CreateToken(user);
            return Ok(token);
        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.email),
                new Claim(ClaimTypes.Role, "Admin")

            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes((string)password));
                return computedHash.SequenceEqual(passwordHash);

            }
        }
    }
}
