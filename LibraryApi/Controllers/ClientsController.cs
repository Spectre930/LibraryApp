using LibraryApp.DataAccess;
using LibraryApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly LibraryContext _db;

        public ClientsController(LibraryContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IEnumerable<Clients>> GetAll()
        {
            var clientList = await _db.Clients.ToListAsync();

            foreach (Clients client in clientList)
            {
                client.Roles = await _db.Roles.FindAsync(client.RolesId);
            }
            return clientList;
        }

        [HttpGet("id")]
        [ProducesResponseType(typeof(Clients), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var client = await _db.Clients.FindAsync(id);
            client.Roles = await _db.Roles.FindAsync(client.RolesId);
            if (client == null)
                return NotFound();

            return Ok(client);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Clients client)
        {

            var user = client;
            user.Roles = await _db.Roles.FindAsync(client.RolesId);
            await _db.Clients.AddAsync(user);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        [HttpPut("id")]
        [ProducesResponseType(typeof(Clients), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, Clients client)
        {
            if (id != client.Id)
                return BadRequest();

            client.Roles = await _db.Roles.FindAsync(client.RolesId);
            _db.Entry(client).State = EntityState.Modified;

            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var ClientToDelete = await _db.Clients.FindAsync(id);

            if (ClientToDelete == null)
                return NotFound();


            _db.Clients.Remove(ClientToDelete);
            await _db.SaveChangesAsync();


            return NoContent();
        }

    }
}
