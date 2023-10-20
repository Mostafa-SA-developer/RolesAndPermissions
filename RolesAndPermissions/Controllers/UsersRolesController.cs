using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RolesAndPermissions.DbC;
using RolesAndPermissions.Models;

namespace RolesAndPermissions.Controllers
{
    [Authorize]
    [Route("api/UsersRoles")]
    [ApiController]
    public class UsersRolesController : Controller
    {
        private readonly DbDataContext _context;

        public UsersRolesController(DbDataContext context)
        {
            _context = context;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersRoles>>> GetUsersRoles()=> await _context.UsersRoles.ToListAsync();

       
        [HttpGet("{id}")]
        public async Task<ActionResult<UsersRoles>> GetUsersRoles(int id)
        {
            var usersRoles = await _context.UsersRoles.FindAsync(id);

            if (usersRoles == null)
            {
                return NotFound();
            }

            return usersRoles;
        }

        [HttpPost]
        public async Task<ActionResult<UsersRoles>> PostUsersRoles(UsersRoles usersRoles)
        {
            _context.UsersRoles.Add(usersRoles);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsersRoles", new { id = usersRoles.UsersRolesId }, usersRoles);
        }

      
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsersRoles(int id, UsersRoles usersRoles)
        {
            if (id != usersRoles.UsersRolesId)
            {
                return BadRequest();
            }

            _context.Entry(usersRoles).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersRolesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

  
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsersRoles(int id)
        {
            var usersRoles = await _context.UsersRoles.FindAsync(id);
            if (usersRoles == null)
            {
                return NotFound();
            }

            _context.UsersRoles.Remove(usersRoles);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsersRolesExists(int id)=> _context.UsersRoles.Any(e => e.UsersRolesId == id);
    }
}
