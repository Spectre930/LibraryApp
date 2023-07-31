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
        [HttpGet("{clientId}_{bookId}")]
        [ProducesResponseType(typeof(Purchases), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int clientId, int bookId)
        {
            var purchase = await _unitOfWork.Purchases.GetFirstOrDefaultAsync(x => x.BookId == bookId && x.ClientId == clientId);
            if (purchase == null)
                return NotFound();

            return Ok(purchase);
        }

        // POST api/<PurchasesController>
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(typeof(Purchases), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(PurchasesDto p)
        {
            var purchase = _unitOfWork.Purchases.CreatePurchase(p);
            await _unitOfWork.Purchases.AddAsync(purchase);
            await _unitOfWork.SaveAsync();

            return Ok(p);
        }

        // PUT api/<PurchasesController>/5
        [HttpPut]
        [Route("{clientId}_{bookId}/update")]
        [ProducesResponseType(typeof(Purchases), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int clientId, int bookId, Purchases purchase)
        {

            if (purchase.ClientId == clientId && purchase.BookId == bookId)
            {
                _unitOfWork.Purchases.Update(purchase);
                await _unitOfWork.SaveAsync();
                return NoContent();
            }
            return BadRequest();
        }

        // DELETE api/<PurchasesController>/5
        [HttpDelete]
        [Route("{clientId}_{bookId}/delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int clientId, int bookId)
        {
            var purchaseToDelete = await _unitOfWork.Purchases.GetFirstOrDefaultAsync(x => x.BookId == bookId && x.ClientId == clientId);

            if (purchaseToDelete == null)
                return NotFound();

            _unitOfWork.Purchases.Remove(purchaseToDelete);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}
