using LibraryApp.DataAccess;
using LibraryApp.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly LibraryContext _db;

        public RolesController(LibraryContext db) {
            _db = db;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IEnumerable<Roles>> GetAll() {
            return await _db.Roles.ToListAsync();
        }

        [HttpGet("id")]
        [ProducesResponseType(typeof(Roles), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id) {
            var Role = await _db.Roles.FindAsync(id);
            if (Role == null)
                return NotFound();

            return Ok(Role);
        }

        [HttpPost]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Roles role) {
            await _db.Roles.AddAsync(role);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = role.Id }, role);
        }

        [HttpPut]
        [Route("{id}/update")]
        [ProducesResponseType(typeof(Roles), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id) {

            var role = await _db.Roles.FindAsync(id);
            if (role == null)
                return BadRequest();

            _db.Roles.Update(role);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}/delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id) {
            var RoleToDelete = await _db.Roles.FindAsync(id);

            if (RoleToDelete == null)
                return NotFound();

            _db.Roles.Remove(RoleToDelete);
            await _db.SaveChangesAsync();

            return NoContent();
        }

    }
}
