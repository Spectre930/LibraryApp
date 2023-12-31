﻿
using LibraryApp.DataAccess.Repository.IRepository;
using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;
using Microsoft.AspNetCore.Mvc;


namespace LibraryApi.Controllers.Operations;

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
            x.ReturnDate,
            x.LateReturnFee
        });

        return borrowList;
    }


    [HttpGet("id")]
    [ProducesResponseType(typeof(Borrow), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var borrow = await _unitOfWork.Borrows
                              .GetFirstOrDefaultAsync(x => x.Id == id, new[] { "Book", "Client" });

        if (borrow == null)
            return NotFound();

        return Ok(borrow);
    }


    [HttpPost]
    [Route("create")]
    [ProducesResponseType(typeof(Borrow), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(BorrowDto dto)
    {


        var borrow = await _unitOfWork.Borrows.BorrowBook(dto);

        await _unitOfWork.Borrows.AddAsync(borrow);
        await _unitOfWork.SaveAsync();

        return Ok(borrow);
    }


    [HttpPut]
    [Route("id/update")]
    [ProducesResponseType(typeof(Borrow), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id)
    {
        var borrow = await _unitOfWork.Borrows.GetFirstOrDefaultAsync(x => x.Id == id);

        if (borrow != null)
        {
            _unitOfWork.Borrows.Update(borrow);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
        return BadRequest();
    }


    [HttpDelete]
    [Route("id/delete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var borrowToDelete = await _unitOfWork.Borrows.GetFirstOrDefaultAsync(x => x.Id == id);

        if (borrowToDelete == null)
            return NotFound();

        _unitOfWork.Borrows.Remove(borrowToDelete);
        await _unitOfWork.SaveAsync();

        return NoContent();
    }

    [HttpPost]
    [Route("return/{id}")]
    [ProducesResponseType(typeof(Borrow), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Return(int id)
    {
        var borrow = await _unitOfWork.Borrows.GetFirstOrDefaultAsync(x => x.Id == id, new[] { "Book", "Client" });

        if (borrow != null)
        {
            var b = await _unitOfWork.Borrows.ReturnBook(borrow);
            return Ok(b);
        }
        return NotFound("The book you are returning wasn't borrowed");
    }

}
