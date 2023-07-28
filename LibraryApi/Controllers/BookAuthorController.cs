using LibraryApp.DataAccess;
using LibraryApp.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace LibraryApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookAuthorController : ControllerBase
    {
    private readonly LibraryContext _db;

    public BookAuthorController(LibraryContext db)
        {
        _db = db;
        }


    [HttpGet]
    [Route("getall")]
    public async Task<IEnumerable<AuthorBook>> GetAll()
        {
        return await _db.AuthorBooks.ToListAsync();
        }

    [HttpGet("{bookId}_{authorId}")]
    [ProducesResponseType(typeof(Authors), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int BookId, int AuthorId)
        {
        var bookAuthor = await _db.AuthorBooks.FindAsync(BookId, AuthorId);
        if (bookAuthor == null)
            return NotFound();

        return Ok(bookAuthor);
        }

    [HttpPost]
    [Route("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(AuthorBook ba)
        {
        ba.Author = await _db.Authors.FindAsync(ba.AuthorId);
        ba.Book = await _db.Books.FindAsync(ba.BookId);
        await _db.AuthorBooks.AddAsync(ba);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { bookId = ba.BookId, authorId = ba.AuthorId }, ba);
        }

    [HttpPut]
    [Route("{bookId}_{authorId}/update")]
    [ProducesResponseType(typeof(AuthorBook), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int bookId, int authorId)
        {
        var ba = await _db.AuthorBooks.Include(x => x.Author).Include(x => x.Book)
            .FirstOrDefaultAsync(x => x.BookId == bookId && x.AuthorId == authorId);

        if (ba == null)
            return BadRequest();

        _db.Update(ba);
        await _db.SaveChangesAsync();

        return NoContent();
        }

    [HttpDelete]
    [Route("{bookId}_{authorId}/delete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int bookId, int authorId)
        {
        var baToDelete = await _db.Authors.FindAsync(bookId, authorId);

        if (baToDelete == null)
            return NotFound();

        _db.Authors.Remove(baToDelete);
        await _db.SaveChangesAsync();

        return NoContent();
        }

    }


