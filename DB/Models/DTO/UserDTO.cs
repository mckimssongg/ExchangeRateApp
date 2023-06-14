using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace DB.Models.DTO
{
    public class UserDTO
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string? RolName { get; set; }
        public static async Task<UserDTO> FromUserAsync(DI_MFSD_J_GarciaContext context, User userObj)
        {
            if (userObj == null)
            {
                return null;
            }

            var rol = await context.Rols.SingleOrDefaultAsync(r => r.RolID == userObj.RolRefID);
            if (rol == null)
            {
                return new UserDTO
                {
                    UserID = userObj.UserID,
                    Email = userObj.Email,
                    Name = userObj.Name,
                    RolName = null
                };
            }

            return new UserDTO
            {
                UserID = userObj.UserID,
                Email = userObj.Email,
                Name = userObj.Name,
                RolName = rol.Name
            };
        }
    }
}
