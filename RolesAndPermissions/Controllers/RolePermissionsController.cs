using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RolesAndPermissions.DbC;
using RolesAndPermissions.Models;

namespace RolesAndPermissions.Controllers
{
    [Authorize]
    [Route("api/RolePermissions")]
    [ApiController]
    public class RolePermissionsController : Controller
    {
        private  DbDataContext _context; 

        public RolePermissionsController(DbDataContext context) 
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RolePermissions>>> GetRolePermissions()=> await _context.RolePermissions.ToListAsync();

        [HttpGet("{RolePermissionsId}")]
        public async Task<ActionResult<RolePermissions>> GetRolePermissions(int RolePermissionsId)
        {
            var rolePermissions = await _context.RolePermissions.FindAsync(RolePermissionsId);

            if (rolePermissions == null)
            {
                return NotFound();
            }

            return rolePermissions;
        }

        
        [HttpPost]
        public async Task<ActionResult<RolePermissions>> PostRolePermissions(RolePermissions rolePermissions)
        {
            _context.RolePermissions.Add(rolePermissions);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRolePermissions", new { RolePermissionsId = rolePermissions.RolePermissionsId }, rolePermissions);
        }

  
        [HttpPut("{RolePermissionsId}")]
        public async Task<IActionResult> PutRolePermissions(int RolePermissionsId, RolePermissions rolePermissions)
        {
            if (RolePermissionsId != rolePermissions.RolePermissionsId)
            {
                return BadRequest();
            }

            _context.Entry(rolePermissions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolePermissionsExists(RolePermissionsId))
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

       
        [HttpDelete("{RolePermissionsId}")]
        public async Task<IActionResult> DeleteRolePermissions(int RolePermissionsId)
        {
            var rolePermissions = await _context.RolePermissions.FindAsync(RolePermissionsId);
            if (rolePermissions == null)
            {
                return NotFound();
            }

            _context.RolePermissions.Remove(rolePermissions);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RolePermissionsExists(int RolePermissionsId)=> _context.RolePermissions.Any(e => e.RolePermissionsId == RolePermissionsId);
    }
}
