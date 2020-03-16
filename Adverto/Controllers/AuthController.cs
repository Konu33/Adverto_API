using Adverto.Domain;
using Adverto.Dto.AuthDto;
using Adverto.Options;
using Adverto.Repositories.AuthRepository;
using Adverto.Routes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Adverto.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _configuration;
        public AuthController(IAuthRepository repo,IConfiguration configuration)
        {
            _repo = repo;
            _configuration = configuration;
        }

        [HttpPost(RoutesAPI.Auth.Login)]
        public async Task<IActionResult> Login(UserForAuth user)
        {
            var userToLogin = await _repo.Login(user.Name, user.Password);

            if (userToLogin == null)
                return Unauthorized();


            //Token 
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,userToLogin.Id.ToString()),
                new Claim(ClaimTypes.Name,user.Name),
            };

            var jwtSettings = new JwtSettings();

            _configuration.Bind(nameof(jwtSettings), jwtSettings);


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secrect));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = creds
            };


            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new { token = tokenHandler.WriteToken(token) });
        }
        [HttpPost(RoutesAPI.Auth.Register)]
        public async Task<IActionResult> Register(UserForAuth user)
        {
            user.Name = user.Name.ToLower();

            if (await _repo.UserCheck(user.Name))
                return BadRequest("User Exist");

            var userToCreate = new User()
            {
                Id = Guid.NewGuid(),
                Name = user.Name,
                isAdmin = false,
            };

            var created = await _repo.Register(userToCreate, user.Password);

            return StatusCode(201);

        }

    }
}
