using DB.Models;
using DB.Models.DTO;
using System.Security.Claims;

namespace APITechTest.Utils
{
    public class Jwt
    {
        public string Key { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public string? Subject { get; set; }

        public static dynamic validarToken(ClaimsIdentity identity)
        {
            try {
                if (identity.Claims.Count() == 0) 
                { 
                    return new { status = false, message = "Invalid token", result = ""};
                }
                var id = identity.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
                var email = identity.Claims.FirstOrDefault(c => c.Type == "Email").Value;
                var rol = identity.Claims.FirstOrDefault(c => c.Type == "RolID").Value;
                var name = identity.Claims.FirstOrDefault(c => c.Type == "Name").Value;

                if (id == null || email == null || rol == null || name == null)
                {
                    return new { status = false, message = "User not found", result = "" };
                }   
                
                return new { status = true, message = "Valid token", result = new { UserID = id, Email = email, RolID = rol, Name = name } };
            }

            catch {
                return new { status = false, message = "Invalid token", result = "" };
            }
        }
    }
}
