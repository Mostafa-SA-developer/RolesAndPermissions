using Microsoft.EntityFrameworkCore;
using RolesAndPermissions.Models;

namespace RolesAndPermissions.DbC
{
    public class DbDataContext : DbContext 
    {
        public DbDataContext(DbContextOptions<DbDataContext> options):base(options)  { 
        
       

        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<RolePermissions> RolePermissions { get; set; }
        public DbSet<UsersRoles> UsersRoles { get; set; }     
    }
}
