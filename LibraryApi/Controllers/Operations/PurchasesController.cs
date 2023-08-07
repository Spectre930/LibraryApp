using LibraryApp.DataAccess;
using LibraryApp.DataAccess.Repository.IRepository;
using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace LibraryApi.Controllers.Operations
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PurchasesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        // GET: api/<PurchasesController>
        [HttpGet]
        [Route("getall")]
        public async Task<IEnumerable<Purchases>> GetAll()
        {
            var purchaseList = await _unitOfWork.Purchases
                                                .GetAllAsync(new[] { "Book", "Client", "Employee" });

            return purchaseList;
        }

        // GET api/<PurchasesController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Purchases), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var purchase = await _unitOfWork.Purchases.GetFirstOrDefaultAsync(x => x.Id == id);
            if (purchase == null)
                return NotFound();

            return Ok(purchase);
        }

        // POST api/<PurchasesController>
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(typeof(Purchases), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Purchases), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(PurchasesDto p)
        {
            var book = await _unitOfWork.Books.GetFirstOrDefaultAsync(x => x.Id == p.BookId, null);

            if (book.AvailableCopies < p.Quantity)
                return BadRequest("The quantity you want is more than the quantity available");

            var purchase = await _unitOfWork.Purchases.CreatePurchase(p);

            book.AvailableCopies = _unitOfWork.Books.SetAvailableCopies(book.Copies, book.Copies - p.Quantity, book.AvailableCopies);
            book.Copies -= p.Quantity;
            await _unitOfWork.Books.UpdateBooks(book);


            await _unitOfWork.Purchases.AddAsync(purchase);
            await _unitOfWork.SaveAsync();

            return Ok(purchase);
        }

        // PUT api/<PurchasesController>/5
        [HttpPut]
        [Route("{id}/update")]
        [ProducesResponseType(typeof(Purchases), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, Purchases purchase)
        {

            if (purchase.Id == id)
            {
                _unitOfWork.Purchases.Update(purchase);
                await _unitOfWork.SaveAsync();
                return NoContent();
            }
            return BadRequest();
        }

        // DELETE api/<PurchasesController>/5
        [HttpDelete]
        [Route("{id}/delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var purchaseToDelete = await _unitOfWork.Purchases.GetFirstOrDefaultAsync(x => x.Id == id);

            if (purchaseToDelete == null)
                return NotFound();

            _unitOfWork.Purchases.Remove(purchaseToDelete);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}
