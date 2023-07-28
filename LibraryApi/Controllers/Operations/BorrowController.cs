using LibraryApp.DataAccess;
using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;
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

        public BorrowController(LibraryContext db) {
            _db = db;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IEnumerable<Borrow>> GetAll() {
            var borrowList = await _db.Borrows
                                   .Include(x => x.Book)
                                   .Include(x => x.Client)
                                   .Select(x => new
                                   {
                                       x.ClientId,
                                       x.BookId,
                                       x.BorrowDate,
                                       x.ReturnDate
                                   }).ToListAsync();
            return await _db.Borrows.ToListAsync();
        }

        // GET api/<PurchasesController>/5
        [HttpGet("{BookId}_{ClientId}")]
        [ProducesResponseType(typeof(Borrow), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int BookId, int ClientId) {
            var borrow = await _db.Borrows
                                   .Include(x => x.Book)
                                   .Include(x => x.Client)
                                   .Select(x => new
                                   {
                                       x.ClientId,
                                       x.BookId,
                                       x.BorrowDate,
                                       x.ReturnDate
                                   })
                                  .FirstOrDefaultAsync(x => x.BookId == BookId && x.ClientId == ClientId);

            if (borrow == null)
                return NotFound();



            return Ok(borrow);
        }

        // POST api/<PurchasesController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(BorrowDto dto) {

            var borrow = new Borrow
            {
                BookId = dto.BookId,
                ClientId = dto.ClientId,
                BorrowDate = dto.BorrowDate,
                ReturnDate = dto.ReturnDate,
            };

            await _db.Borrows.AddAsync(borrow);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { clientId = borrow.ClientId, bookId = borrow.BookId }, borrow);
        }


        [HttpPut]
        [Route("{bookId}_{clientId}/update")]
        [ProducesResponseType(typeof(Borrow), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int clientId, int bookId) {
            var borrow = await _db.Borrows
                                          .Include(x => x.Book)
                                          .Include(x => x.Client)
                                          .FirstOrDefaultAsync(x => x.BookId == bookId && x.ClientId == clientId);

            if (borrow == null)
                return BadRequest();

            _db.Borrows.Update(borrow);
            await _db.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete]
        [Route("{bookId}_{clientId}/delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int clientId, int bookId) {
            var borrowToDelete = await _db.Borrows.FirstOrDefaultAsync(x => x.BookId == bookId && x.ClientId == clientId);

            if (borrowToDelete == null)
                return NotFound();

            _db.Borrows.Remove(borrowToDelete);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
