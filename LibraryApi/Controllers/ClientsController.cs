using LibraryApp.DataAccess;
using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;
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

        public ClientsController(LibraryContext db) {
            _db = db;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IEnumerable<Clients>> GetAll() {
            var clientList = await _db.Clients.Include(x => x.Roles).ToListAsync();
            return clientList;
        }

        [HttpGet("id")]
        [ProducesResponseType(typeof(Clients), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id) {
            var client = await _db.Clients.Include(x => x.Roles).FirstOrDefaultAsync(x => x.Id == id);

            if (client == null)
                return NotFound();

            return Ok(client);
        }

        [HttpPost]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(ClientsDto client) {

            var user = new Clients
            {
                F_Name = client.F_Name,
                L_Name = client.L_Name,
                Email = client.Email,
                DOB = client.DOB,
                Age = client.Age,
                RolesId = client.RolesId
            };
            await _db.Clients.AddAsync(user);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        [HttpPut]
        [Route("{id}/update")]
        [ProducesResponseType(typeof(Clients), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id) {
            var client = await _db.Clients.FindAsync(id);

            if (client == null)
                return BadRequest();

            _db.Clients.Update(client);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}/delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id) {
            var ClientToDelete = await _db.Clients.FindAsync(id);

            if (ClientToDelete == null)
                return NotFound();


            _db.Clients.Remove(ClientToDelete);
            await _db.SaveChangesAsync();


            return NoContent();
        }

    }
}
