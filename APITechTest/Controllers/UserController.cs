using APITechTest.Utils;
using DB;
using DB.Models;
using DB.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace APITechTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IConfiguration _configuration;
        private DI_MFSD_J_GarciaContext _context;

        public UserController(DI_MFSD_J_GarciaContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        [HttpGet]
        [ProducesResponseType(typeof(Response<List<UserDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<IActionResult> GetUsers()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var result = Jwt.validarToken(identity);
            var rsp = new Response<List<UserDTO>>();

            if (result.status == false)
            {
                rsp.status = false;
                rsp.message = result.message;
                return Ok(rsp);
            }

            try
            {
                var dataToken = result.result;
                int id = Convert.ToInt32(dataToken.UserID);
                int idRol = Convert.ToInt32(dataToken.RolID);
                var currentUser = await _context.Users.FirstOrDefaultAsync(u => u.UserID == id);

                if (currentUser != null)
                {
                    //Role validation admin 
                    if (idRol == 1)
                    {
                        var users = await _context.Users.ToListAsync();

                        var usersDto = users.Select(async u => await UserDTO.FromUserAsync(_context, u)).ToList();

                        var listsersDto = new List<UserDTO>();

                        foreach (var userDto in usersDto)
                        {
                            listsersDto.Add(userDto.Result);
                        }

                        rsp.status = true;
                        rsp.message = "Users found";
                        rsp.value = listsersDto;
                        return Ok(rsp);
                    }
                    else
                    {
                        rsp.status = false;
                        rsp.message = "User does not have permissions";
                        return Ok(rsp);
                    }
                }

                rsp.status = false;
                rsp.message = "User not found";
                return Ok(rsp);
            }

            catch (Exception e)
            {
                rsp.status = false;
                rsp.message = "Error: User not found";
                return Ok(rsp);
            }

        }


        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(Response<Session>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] LoginDTO user)
        {
            var rsp = new Response<Session>();

            if (await _context.Users.AnyAsync(u => u.Email == user.Email && u.Password == user.Password))
            {
                // Verificar si el correo electrónico es válido
                if (!user.Email.EndsWith("@dto.com"))
                {
                    rsp.status = false;
                    rsp.message = "Correo electrónico inválido";
                    return Ok(rsp);
                }

                var userObj = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
                if (userObj != null)
                {
                    var userDto = await UserDTO.FromUserAsync(_context, userObj);
                    var token = await GenerateJwtToken(userObj);
                    if (token != null)
                    {
                        // Login successful
                        var response = new Session
                        {
                            Token = token,
                            User = userDto
                        };
                        rsp.status = true;
                        rsp.message = "User found";
                        rsp.value = response;
                        return Ok(rsp);
                    }
                }

                rsp.status = false;
                rsp.message = "User not found";
                return Ok(rsp);
            }
            else
            {
                rsp.status = false;
                rsp.message = "User not found";
                return Ok(rsp);
            }
        }


        private async Task<string> GenerateJwtToken(User user)
        {
            if (user == null) {
                return "";
            }

            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();

            if (jwt == null) {
                return "";
            }

            var rol = await _context.Rols.SingleOrDefaultAsync(r => r.RolID == user.RolRefID);
           
            if (rol == null)
            {
                return "";
            }

            var claims = new[]
            {
                new Claim("Name", user.Name),
                new Claim("UserID", user.UserID.ToString()),
                new Claim("Email", user.Email),
                new Claim("RolID", rol.RolID.ToString()),
            };

            // Sign the JWT token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: jwt.Issuer,
                audience: jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: signIn
                );

            var tokenWrite = new JwtSecurityTokenHandler().WriteToken(token);

            if (tokenWrite == null) {
                return "";
            }

            return tokenWrite;
        }
    }


}
