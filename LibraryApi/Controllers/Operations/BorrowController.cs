using LibraryApp.DataAccess;
using LibraryApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace LibraryApi.Controllers.Operations
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowController : ControllerBase
    {
        private readonly LibraryContext _db;

        public BorrowController(LibraryContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IEnumerable<Borrow>> GetAll()
        {
            var borrowList = await _db.Borrows.ToListAsync();

            foreach (var borrow in borrowList)
            {
                borrow.Client = await _db.Clients.FindAsync(borrow.ClientId);
                borrow.Book = await _db.Books.FindAsync(borrow.BookId);
            }
            return await _db.Borrows.ToListAsync();
        }

        // GET api/<PurchasesController>/5
        [HttpGet("{BookId}_{ClientId}")]
        [ProducesResponseType(typeof(Borrow), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get( int BookId,int ClientId)
        {
            var borrow = await _db.Purchases.FindAsync(BookId, ClientId);

            if (borrow == null)
                return NotFound();

            borrow.Client = await _db.Clients.FindAsync(ClientId);
            borrow.Book = await _db.Books.FindAsync(BookId);

            return Ok(borrow);
        }

        // POST api/<PurchasesController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Borrow borrow)
        {
            borrow.Client = await _db.Clients.FindAsync(borrow.ClientId);
            borrow.Book = await _db.Books.FindAsync(borrow.BookId);

            await _db.Borrows.AddAsync(borrow);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { clientId = borrow.ClientId, bookId = borrow.BookId }, borrow);
        }


        [HttpPut("{bookId}_{clientId}")]
        [ProducesResponseType(typeof(Borrow), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int clientId, int bookId, Borrow borrow)
        {
            if (clientId != borrow.ClientId && bookId != borrow.BookId)
                return BadRequest();

            borrow.Client = await _db.Clients.FindAsync(borrow.ClientId);
            borrow.Book = await _db.Books.FindAsync(borrow.BookId);

            _db.Entry(borrow).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("{bookId}_{clientId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int clientId, int bookId)
        {
            var borrowToDelete = await _db.Borrows.FindAsync(clientId, bookId);

            if (borrowToDelete == null)
                return NotFound();

            _db.Borrows.Remove(borrowToDelete);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
