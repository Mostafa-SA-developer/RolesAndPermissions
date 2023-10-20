using System.ComponentModel.DataAnnotations.Schema;

namespace RolesAndPermissions.Models
{
    public class RolePermissions
    {
        public int RolePermissionsId { get; set; }

        public int RoleId { get; set; }
        public Role role { get; set; }
        public int PermissionsId { get; set; }
        public Permissions permissions { get; set; }


    }
}

