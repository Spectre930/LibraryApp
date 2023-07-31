using LibraryApp.DataAccess;
using LibraryApp.DataAccess.Repository.IRepository;
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
        private readonly IUnitOfWork _unitOfWork;

        public BorrowController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IEnumerable<Borrow>> GetAll()
        {
            var borrowList = await _unitOfWork.Borrows.GetAllAsync(new[] { "Book", "Client" });
            borrowList.Select(x => new
            {
                x.ClientId,
                x.BookId,
                x.BorrowDate,
                x.ReturnDate
            });

            return borrowList;
        }

        // GET api/<PurchasesController>/5
        [HttpGet("{BookId}_{ClientId}")]
        [ProducesResponseType(typeof(Borrow), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int BookId, int ClientId)
        {
            var borrow = await _unitOfWork.Borrows
                                  .GetFirstOrDefaultAsync(x => x.BookId == BookId && x.ClientId == ClientId, new[] { "Book", "Client" });

            if (borrow == null)
                return NotFound();

            return Ok(borrow);
        }

        // POST api/<PurchasesController>
        [HttpPost]
        [ProducesResponseType(typeof(Borrow), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(BorrowDto dto)
        {

            var borrow = new Borrow
            {
                BookId = dto.BookId,
                ClientId = dto.ClientId,
                BorrowDate = dto.BorrowDate,
                ReturnDate = dto.ReturnDate,
            };

            await _unitOfWork.Borrows.AddAsync(borrow);
            await _unitOfWork.SaveAsync();

            return Ok(dto);
        }


        [HttpPut]
        [Route("{bookId}_{clientId}/update")]
        [ProducesResponseType(typeof(Borrow), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int clientId, int bookId, Borrow borrow)
        {


            if (borrow.ClientId == clientId && borrow.BookId == bookId)
            {
                _unitOfWork.Borrows.Update(borrow);
                await _unitOfWork.SaveAsync();
                return NoContent();
            }
            return BadRequest();
        }


        [HttpDelete]
        [Route("{bookId}_{clientId}/delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int clientId, int bookId)
        {
            var borrowToDelete = await _unitOfWork.Borrows.GetFirstOrDefaultAsync(x => x.BookId == bookId && x.ClientId == clientId);

            if (borrowToDelete == null)
                return NotFound();

            _unitOfWork.Borrows.Remove(borrowToDelete);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}
