using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RolesAndPermissions.DbC;
using RolesAndPermissions.Models;
using System;

namespace RolesAndPermissions.Controllers
{
    [Authorize]
    [Route("api/Role")]
    [ApiController]
    public class RoleController : Controller
    {

        private DbDataContext _context;

        public RoleController(DbDataContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()=> await _context.Roles.ToListAsync();
        [HttpGet("{RoleId}")]
        public async Task<ActionResult<Role>> GetRole(int RoleId)
        {
            var role = await _context.Roles.FindAsync(RoleId);

            if (role == null)
            {
                return NotFound();
            }

            return role;
        }
        [HttpPost]
        public async Task<ActionResult<Role>> CreateRole(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRole", new { RoleId = role.RoleId }, role);
        }

        [HttpPut("{RoleRoleId}")]
        public async Task<IActionResult> UpdateRole(int RoleId, Role role)
        {
            if (RoleId != role.RoleId)
            {
                return BadRequest();
            }

            _context.Entry(role).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(RoleId))
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
        [HttpDelete("{RoleId}")]
        public async Task<IActionResult> DeleteRole(int RoleId)
        {
            var role = await _context.Roles.FindAsync(RoleId);
            if (role == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoleExists(int RoleId) => _context.Roles.Any(e => e.RoleId == RoleId);
    }
}
