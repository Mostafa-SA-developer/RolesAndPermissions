using System.ComponentModel.DataAnnotations;

namespace RolesAndPermissions.Models
{
    public class Role
    {
        public int RoleId { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}
