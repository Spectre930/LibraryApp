using LibraryApp.DataAccess.Repository.IRepository;
using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;


namespace LibraryApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin,Employee")]
public class BookAuthorController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;



    public BookAuthorController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    [HttpGet]
    [Route("getall")]
    public async Task<IEnumerable<AuthorBook>> GetAll()
    {
        return await _unitOfWork.AuthorBook.GetAllAsync();
    }

    [HttpGet("{bookId}_{authorId}")]
    [ProducesResponseType(typeof(Authors), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int BookId, int AuthorId)
    {
        var bookAuthor = await _unitOfWork.AuthorBook.GetFirstOrDefaultAsync(x => x.BookId == BookId && x.AuthorId == AuthorId);

        if (bookAuthor == null)
            return NotFound();

        return Ok(bookAuthor);
    }

    [HttpPost]
    [Route("create")]
    [ProducesResponseType(typeof(AuthorBook), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(AuthorBookDto ba)
    {
        var bookAuthor = new AuthorBook
        {
            AuthorId = ba.AuthId,
            BookId = ba.BookId,
        };
        await _unitOfWork.AuthorBook.AddAsync(bookAuthor);
        await _unitOfWork.SaveAsync();

        return Ok(bookAuthor);
    }
    [HttpPut]
    [Route("update/{bookId}_{authorId}")]
    [ProducesResponseType(typeof(AuthorBook), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int bookId, int authorId, AuthorBook ba)
    {


        if (ba.AuthorId != authorId || ba.BookId != bookId)
            return BadRequest();

        _unitOfWork.AuthorBook.Update(ba);
        await _unitOfWork.SaveAsync();

        return NoContent();
    }

    [HttpDelete]
    [Route("{bookId}_{authorId}/delete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int bookId, int authorId)
    {
        var baToDelete = await _unitOfWork.AuthorBook.GetFirstOrDefaultAsync(x => x.BookId == bookId && x.AuthorId == authorId);

        if (baToDelete == null)
            return NotFound();

        _unitOfWork.AuthorBook.Remove(baToDelete);
        await _unitOfWork.SaveAsync();

        return NoContent();
    }

}


