using Microsoft.EntityFrameworkCore;
using RolesAndPermissions.DbC;
using RolesAndPermissions.Models;

namespace RolesAndPermissions.helper
{
    public class MethodsManager
    {

        public static bool checkPermission(DbDataContext context,string perStr,int userId)
        {
            IEnumerable<UsersRoles> roles = context.UsersRoles.Where(x => x.UsersId == userId).ToList();

            if (roles.Count() == 0)
            {
                return false;
            }

            IEnumerable<RolePermissions> AllrolePermissions = context.RolePermissions.ToList();
            IEnumerable<Permissions> AllPermissions = context.Permissions.ToList();

            foreach (UsersRoles role in roles)
            {
                IEnumerable<RolePermissions> rolePermissions = AllrolePermissions.Where(x => x.RoleId == role.RoleId).ToList();
                foreach(RolePermissions rolePermission in rolePermissions) {

                    var res = AllPermissions.Where(x => $"{x.typePermissions}-{x.appsName}"==perStr).FirstOrDefault();
                    if (res != null)
                    {
                        return true;
                    }

                }
            }

            return false;
        }
    }
}
