using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RolesAndPermissions.DbC;
using RolesAndPermissions.Models;

namespace RolesAndPermissions.Controllers
{
    [Authorize]
    [Route("api/Permissions")]
    [ApiController]
    public class PermissionsController : Controller
    {
        private DbDataContext _context;

       public PermissionsController(DbDataContext _dataContext)
        {
            this._context = _dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Permissions>>>  getAllpermission() => await _context.Permissions.ToListAsync();

        [HttpGet("{PermissionsId}")]
        public async Task<ActionResult<Permissions>> GetPermissions(int PermissionsId)
        {
            var role = await _context.Permissions.FindAsync(PermissionsId);

            if (role == null)
            {
                return NotFound();
            }

            return role;
        }
        [HttpPost]
        public async Task<ActionResult<Permissions>> CreatePermissions(Permissions permissions)
        {
            _context.Permissions.Add(permissions);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPermissions", new { PermissionsId = permissions.PermissionsId }, permissions);
        }

        [HttpPut("{PermissionsId}")]
        public async Task<IActionResult> UpdatePermissions(int PermissionsId, Permissions permissions)
        {
            if (PermissionsId != permissions.PermissionsId)
            {
                return BadRequest();
            }

            _context.Entry(permissions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PermissionsExists(PermissionsId))
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
        [HttpDelete("{PermissionsId}")]
        public async Task<IActionResult> DeletePermissions(int PermissionsId)
        {
            var permissions = await _context.Permissions.FindAsync(PermissionsId);
            if (permissions == null)
            {
                return NotFound();
            }

            _context.Permissions.Remove(permissions);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PermissionsExists(int PermissionsId) => _context.Permissions.Any(e => e.PermissionsId == PermissionsId);

    }
}
