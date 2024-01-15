
using AutoMapper;
using Food_Users_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Food_Users_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IConfiguration _configuration;
        private readonly UserDBContext _userDb;
        private readonly IMapper _mapper;

        public AuthController(IConfiguration configuration, UserDBContext userDb, IMapper _mapper)
        {
            _configuration = configuration;
            _userDb = userDb;
            this._mapper = _mapper;
        }



        [HttpPost("Register")]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(UserDto), 200)]
        public async Task<ActionResult> Register(RegisterDto newUser)
        {
            if (newUser.Name == null)
            {
                return BadRequest("Please Enter Name");
            }

            if (await _userDb.Users.AnyAsync(u => u.Email == newUser.Email))
            {
                return BadRequest("Enter Another Email");
            };


            var user = _mapper.Map<User>(newUser);

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newUser.PasswordHash);

            await _userDb.AddAsync(user);
            await _userDb.SaveChangesAsync();

            return Ok(newUser);

        }

        [HttpPost("Login")]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<ActionResult> Login(LoginDto _user)
        {

            var user = await _userDb.Users.FirstOrDefaultAsync(u => u.Email == _user.Email);

            if (user == null)
            {
                return BadRequest("Email Is Incorrect");
            }

            if (!BCrypt.Net.BCrypt.Verify(_user.Password, user.PasswordHash))
            {
                return BadRequest("Invalid Password");
            }

            var Token = token(user);

            await _userDb.SaveChangesAsync();
            return Ok(Token);

        }

        private string token(User user)
        {

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_configuration.GetSection("JWT:Token").Value!));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken
                (
                   claims: claims,
                   expires: DateTime.Now.AddHours(1),
                   signingCredentials: cred
                );

            var Jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return Jwt;
        }


    }
}
