using System.ComponentModel.DataAnnotations;

namespace RolesAndPermissions.Models
{
    public class Permissions
    {
        public int PermissionsId { get; set; }

        [Required]
        public string Name { get; set; }

        //this type should use the enum as list in user interface to know the type of permission
        //see /helpr/enum/typePermissions
        public int typePermissions { get; set; }
        //this type should use the enum as list in user interface to know the type of permission
        //see /helpr/enum/appsPermissions
        public int appsName { get; set; }

        public string? Description { get; set; }
    }
}
