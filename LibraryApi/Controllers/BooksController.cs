using LibraryApp.DataAccess;
using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibraryContext _db;

        public BooksController(LibraryContext db) {
            _db = db;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IEnumerable<Books>> GetAll() {
            var bookList = await _db.Books.Include(x => x.Genre.Name).ToListAsync();
            return await _db.Books.ToListAsync();
        }

        [HttpGet("id")]
        [ProducesResponseType(typeof(Books), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id) {
            var book = await _db.Books.Include(x => x.Genre).FirstOrDefaultAsync(x => x.Id == id);

            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpPost]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(BooksDTO dto) {
            var book = new Books
            {
                Title = dto.Title,
                Description = dto.Description,
                GenreId = dto.GenreId,
                Copies = dto.Copies,
                AvailableCopies = dto.Copies,
                AuthPrice = dto.AuthPrice,
                Author = dto.Author
            };
            await _db.Books.AddAsync(book);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = book.Id }, book);
        }

        [HttpPut]
        [Route("{id}/update")]
        [ProducesResponseType(typeof(Books), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id) {
            var book = await _db.Books.Include(x => x.Genre).FirstOrDefaultAsync(x => x.Id == id);
            if (book == null)
                return BadRequest();

            _db.Update(book);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}/delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id) {
            var bookToDelete = await _db.Books.FindAsync(id);

            if (bookToDelete == null)
                return NotFound();

            _db.Books.Remove(bookToDelete);
            await _db.SaveChangesAsync();

            return NoContent();
        }

    }
}
