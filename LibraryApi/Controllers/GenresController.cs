using LibraryApp.DataAccess;
using LibraryApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly LibraryContext _db;

        public GenresController(LibraryContext db)
        {
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Genres), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IEnumerable<Genres>> GetAll()
        {
            var allGenres = await _db.Genres.ToListAsync();
            return allGenres;
        }

        [HttpGet("id")]
        [ProducesResponseType(typeof(Genres), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {

            var genre = await _db.Genres.FindAsync(id);

            if (genre == null)
                return NotFound();

            return Ok(genre);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Genres genre)
        {
            await _db.Genres.AddAsync(genre);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = genre.Id }, genre);
        }

        [HttpPut("id")]
        [ProducesResponseType(typeof(Genres), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, Genres genre)
        {

            if (id != genre.Id)
                return BadRequest();

            _db.Entry(genre).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("id")]
        [ProducesResponseType(typeof(Genres), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var genreToDelete = await _db.Genres.FindAsync(id);
            if (genreToDelete == null)
                return NotFound();

            _db.Genres.Remove(genreToDelete);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
