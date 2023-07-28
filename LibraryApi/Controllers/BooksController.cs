using LibraryApp.DataAccess;
using LibraryApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibraryContext _db;

        public BooksController(LibraryContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IEnumerable<Books>> GetAll()
        {
            var bookList = await _db.Books.ToListAsync();

            foreach (Books b in bookList)
                b.Genre = await _db.Genres.FindAsync(b.GenreId);

            return await _db.Books.ToListAsync();
        }

        [HttpGet("id")]
        [ProducesResponseType(typeof(Books), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var book = await _db.Books.FindAsync(id);
            book.Genre = await _db.Genres.FindAsync(book.GenreId);
            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Books book)
        {
            var genre = await _db.Genres.FindAsync(book.GenreId);

            if (genre == null)
            {
                book.GenreId = 1;
                book.Genre = await _db.Genres.FindAsync(1);
            }
            else
            {
                book.Genre = genre;
            }
            book.AvailableCopies = book.Copies;
            await _db.Books.AddAsync(book);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = book.Id }, book);
        }

        [HttpPut("id")]
        [ProducesResponseType(typeof(Books), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, Books book)
        {
            if (id != book.Id)
                return BadRequest();
            book.Genre = await _db.Genres.FindAsync(book.GenreId);

            _db.Entry(book).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var bookToDelete = await _db.Books.FindAsync(id);

            if (bookToDelete == null)
                return NotFound();

            _db.Books.Remove(bookToDelete);
            await _db.SaveChangesAsync();

            return NoContent();
        }

    }
}
