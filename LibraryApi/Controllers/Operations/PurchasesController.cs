using LibraryApp.DataAccess;
using LibraryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryApi.Controllers.Operations
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly LibraryContext _db;

        public PurchasesController(LibraryContext db)
        {
            _db = db;
        }
        // GET: api/<PurchasesController>
        [HttpGet]
        public async Task<IEnumerable<Purchases>> GetAll()
        {
            var purchaseList = await _db.Purchases.ToListAsync();

            foreach (var purchase in purchaseList)
            {
                purchase.Book = await _db.Books.FindAsync(purchase.BookId);
                purchase.Employee = await _db.Employees.FindAsync(purchase.EmployeeId);
                purchase.Client = await _db.Clients.FindAsync(purchase.ClientId);
            }
            return purchaseList;
        }

        // GET api/<PurchasesController>/5
        [HttpGet("{clientId}_{bookId}")]
        [ProducesResponseType(typeof(Purchases), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int clientId, int bookId)
        {
            var purchase = await _db.Purchases.FindAsync(clientId, bookId);

            if (purchase == null)
                return NotFound();

            return Ok(purchase);
        }

        // POST api/<PurchasesController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Purchases purchase)
        {
            purchase.Book = await _db.Books.FindAsync(purchase.BookId);
            purchase.Employee = await _db.Employees.FindAsync(purchase.EmployeeId);
            purchase.Client = await _db.Clients.FindAsync(purchase.ClientId);
            int price = purchase.Book.AuthPrice;
            purchase.BuyPrice = price + (price * 20 / 100);
            await _db.Purchases.AddAsync(purchase);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { clientId = purchase.ClientId, bookId = purchase.BookId }, purchase);
        }

        // PUT api/<PurchasesController>/5
        [HttpPut("{clientId}_{bookId}")]
        [ProducesResponseType(typeof(Purchases), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int clientId, int bookId, Purchases purchase)
        {
            if (clientId != purchase.ClientId && bookId != purchase.BookId)
                return BadRequest();

            purchase.Book = await _db.Books.FindAsync(purchase.BookId);
            purchase.Employee = await _db.Employees.FindAsync(purchase.EmployeeId);
            purchase.Client = await _db.Clients.FindAsync(purchase.ClientId);
            _db.Entry(purchase).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/<PurchasesController>/5
        [HttpDelete("{clientId}_{bookId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int clientId, int bookId)
        {
            var purchaseToDelete = await _db.Purchases.FindAsync(clientId, bookId);

            if (purchaseToDelete == null)
                return NotFound();

            _db.Purchases.Remove(purchaseToDelete);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
