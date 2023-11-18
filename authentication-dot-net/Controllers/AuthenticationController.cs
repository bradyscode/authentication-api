using authentication_dot_net.Interfaces;
using authentication_dot_net.Interfaces.UserInterface;
using authentication_dot_net.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace authentication_dot_net.Controllers
{
    [ApiController]
    public class AuthenticationController : Controller
    {
        private IUserInterface _userInterface;
        private ILogger<AuthenticationController> _logger;
        private IConfiguration _configuration;

        public AuthenticationController(IUserInterface userInterface, ILogger<AuthenticationController> logger, IConfiguration configuration)
        {
            _userInterface = userInterface;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost("/CreateUser")]
        public object CreateUser([FromBody]UserDTO userDTO)
        {
            _logger.LogInformation("Entering /CreateUser");
            if (_userInterface.UsernameExists(userDTO.Username)) throw new Exception("User already exists");
            User user;
            if(userDTO.Permission != null)
            {
                user = new User(userDTO.Username, userDTO.Password, userDTO.Permission);
            }else
            {
                user = new User(userDTO.Username, userDTO.Password);
            }
            var returnValue = _userInterface.CreateUser(user);
            return returnValue;
        }

        [AllowAnonymous]
        [HttpPost("/AuthenticateUser")]
        public async Task<IActionResult> AuthenticateUserAsync([FromBody]UserDTO user)
        {
            var response = Unauthorized();

            var userAuthorized = await _userInterface.AuthenticateUser(user.Username, user.Password);
            if (userAuthorized)
            {
                var issuer = _configuration["Jwt:Issuer"];
                var audience = _configuration["Jwt:Audience"];
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
                var signingCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature
                );

                var subject = new ClaimsIdentity(new[]
                {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                        new Claim(JwtRegisteredClaimNames.Email, user.Username),
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

                return Ok(jwtToken);
            }
            return response;
        }
    }
}
