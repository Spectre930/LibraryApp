using LibraryApp.DataAccess;
using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;
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

        public PurchasesController(LibraryContext db) {
            _db = db;
        }
        // GET: api/<PurchasesController>
        [HttpGet]
        [Route("getall")]
        public async Task<IEnumerable<Purchases>> GetAll() {
            var purchaseList = await _db.Purchases
                                        .Include(x => x.Client)
                                        .Include(x => x.Book)
                                        .Include(x => x.Employee)
                                        .ToListAsync();


            return purchaseList;
        }

        // GET api/<PurchasesController>/5
        [HttpGet("{clientId}_{bookId}")]
        [ProducesResponseType(typeof(Purchases), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int clientId, int bookId) {
            var purchase = await _db.Purchases.FirstOrDefaultAsync(x => x.BookId == bookId && x.ClientId == clientId);
            if (purchase == null)
                return NotFound();

            return Ok(purchase);
        }

        // POST api/<PurchasesController>
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(PurchasesDto p) {

            var purchase = new Purchases
            {
                ClientId = p.ClientId,
                BookId = p.BookId,
                EmployeeId = p.EmployeeId,
                Quantity = p.Quantity,
            };
            int price = purchase.Book.AuthPrice;
            purchase.BuyPrice = price + (price * 20 / 100);

            await _db.Purchases.AddAsync(purchase);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { clientId = purchase.ClientId, bookId = purchase.BookId }, purchase);
        }

        // PUT api/<PurchasesController>/5
        [HttpPut]
        [Route("{clientId}_{bookId}/update")]
        [ProducesResponseType(typeof(Purchases), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int clientId, int bookId) {
            var purchase = await _db.Purchases.FirstOrDefaultAsync(x => x.BookId == bookId && x.ClientId == clientId);

            if (purchase == null)
                return BadRequest();

            _db.Purchases.Update(purchase);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/<PurchasesController>/5
        [HttpDelete]
        [Route("{clientId}_{bookId}/delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int clientId, int bookId) {
            var purchaseToDelete = await _db.Purchases.FirstOrDefaultAsync(x => x.BookId == bookId && x.ClientId == clientId);

            if (purchaseToDelete == null)
                return NotFound();

            _db.Purchases.Remove(purchaseToDelete);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
