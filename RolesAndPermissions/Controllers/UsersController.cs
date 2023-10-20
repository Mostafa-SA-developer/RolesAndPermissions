using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RolesAndPermissions.DbC;
using RolesAndPermissions.helper;
using RolesAndPermissions.Models;

namespace RolesAndPermissions.Controllers
{

    [Authorize]
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DbDataContext _context;
        private Users user;
        public UsersController(DbDataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers() {

            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }
            else initUser();
            string permission = typePermissions.view.ToString()+appsPermissions.Users.ToString();

            bool res=MethodsManager.checkPermission(_context,permission,user.UserId);

            if (!res)
            {
                permission = typePermissions.All.ToString() + appsPermissions.Users.ToString();
                bool restow = MethodsManager.checkPermission(_context, permission, user.UserId);
                if (!restow)
                {
                    return Unauthorized();
                }
               else await _context.Users.ToListAsync();
            }
            else 
            return await _context.Users.ToListAsync();

            return Unauthorized();
        }
        
        [HttpGet("{UserId}")]
        [ProducesResponseType(typeof(Users),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Users>> GetUserByUserId(int UserId)
        {


            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }
            else initUser();
            string permission = typePermissions.view.ToString() + appsPermissions.Users.ToString();

            bool res = MethodsManager.checkPermission(_context, permission, user.UserId);

            if (!res)
            {
                permission = typePermissions.All.ToString() + appsPermissions.Users.ToString();
                bool restow = MethodsManager.checkPermission(_context, permission, user.UserId);
                if (!restow)
                {
                    return Unauthorized();
                }
                else {
                    var user = await _context.Users.FindAsync(UserId);
                    return user == null ? NotFound() : Ok(user);
                }
            }
            else
                 {
                var user = await _context.Users.FindAsync(UserId);
                return user == null ? NotFound() : Ok(user);
            }
            
        }


        [HttpPost]
        [ProducesResponseType(typeof(Users), StatusCodes.Status201Created)]
        public async Task<ActionResult<Users>> CreateUser(Users user)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }
            else initUser();
            string permission = typePermissions.Create.ToString() + appsPermissions.Users.ToString();

            bool res = MethodsManager.checkPermission(_context, permission, user.UserId);

            if (!res)
            {
                permission = typePermissions.All.ToString() + appsPermissions.Users.ToString();
                bool restow = MethodsManager.checkPermission(_context, permission, user.UserId);
                if (!restow)
                {
                    return Unauthorized();
                }
                else
                {
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(GetUserByUserId), new { UserId = user.UserId }, user);
                }
            }
            else
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetUserByUserId), new { UserId = user.UserId }, user);
            }

           


            
        }

        [HttpPut("{UserId}")]
        public async Task<IActionResult> UpdateUser(int UserId, Users user)
        {
            if (UserId != user.UserId)
            {
                return NotFound();
            }

            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }
            else initUser();
            string permission = typePermissions.Update.ToString() + appsPermissions.Users.ToString();

            bool res = MethodsManager.checkPermission(_context, permission, user.UserId);

            if (!res)
            {
                permission = typePermissions.All.ToString() + appsPermissions.Users.ToString();
                bool restow = MethodsManager.checkPermission(_context, permission, user.UserId);
                if (!restow)
                {
                    return Unauthorized();
                }
                else
                {
                    _context.Entry(user).State = EntityState.Modified;
                     await _context.SaveChangesAsync();
                   
                }
            }
            else
            {
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }



            return Unauthorized();

        }

        [HttpDelete("{UserId}")]
        public async Task<IActionResult> DeleteUser(int UserId)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }
            else initUser();
            string permission = typePermissions.Delete.ToString() + appsPermissions.Users.ToString();

            bool res = MethodsManager.checkPermission(_context, permission, user.UserId);

            if (!res)
            {
                permission = typePermissions.All.ToString() + appsPermissions.Users.ToString();
                bool restow = MethodsManager.checkPermission(_context, permission, user.UserId);
                if (!restow)
                {
                    return Unauthorized();
                }
                else
                {
                   

                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }

            return Unauthorized();


        }

        private bool UserExists(int UserId) => _context.Users.Any(e => e.UserId == UserId);

        private void initUser()
        {
           
            string email = HttpContext.User.Claims.ToList()[0].Value;
            Users users = _context.Users.Where(x => x.Email == email).FirstOrDefault();
            if (users != null)
            {
                this.user = users;
            }
        }

       
    }
}
