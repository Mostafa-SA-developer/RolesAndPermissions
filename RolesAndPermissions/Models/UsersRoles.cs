using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RolesAndPermissions.Models
{
    public class UsersRoles
    {
        [Key]
        public int UsersRolesId { get; set; }
      
        public int UsersId { get; set; }
        public int RoleId { get; set; }
    }
}
