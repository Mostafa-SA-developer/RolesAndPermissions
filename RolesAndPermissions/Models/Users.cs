using System.ComponentModel.DataAnnotations;

namespace RolesAndPermissions.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
       
        virtual public ICollection<UsersRoles> UserRoles { get; set; }

    }
}
