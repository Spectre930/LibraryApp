using LibraryApp.DataAccess.Repository.IRepository;
using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;

        public BooksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IEnumerable<Books>> GetAll()
        {


            return await _unitOfWork.Books.GetAllAsync(new[] { "Genre" });

        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Books), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var book = await _unitOfWork.Books.GetFirstOrDefaultAsync(x => x.Id == id, new[] { "Genre" });

            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpPost]
        [Route("create")]
        [ProducesResponseType(typeof(Books), StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Create(BooksDTO dto)
        {
            if (dto.AuthorIds == null)
                return BadRequest("The book should have at least one author");


            var book = new Books
            {
                Title = dto.Title,
                Description = dto.Description,
                GenreId = dto.GenreId,
                Copies = dto.Copies,
                AvailableCopies = dto.Copies,
                AuthPrice = dto.AuthPrice,
                ListedPrice = _unitOfWork.Books.SetBookPrice(dto.AuthPrice, dto.ListedPrice),
            };
            await _unitOfWork.Books.AddAsync(book);
            await _unitOfWork.SaveAsync();

            await _unitOfWork.AuthorBook.AddAuthorBookAsync(dto.AuthorIds, book.Id);
            await _unitOfWork.SaveAsync();
            return Ok(book);
        }

        [HttpPut]
        [Route("update/{id}")]
        [ProducesResponseType(typeof(Books), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Update(Books book)
        {

            if (await _unitOfWork.Books.GetFirstOrDefaultAsync(b => b.Id == book.Id) == null)
                return BadRequest();

            await _unitOfWork.Books.UpdateBooks(book);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Delete(int id)
        {
            var bookToDelete = await _unitOfWork.Books.GetFirstOrDefaultAsync(x => x.Id == id);

            if (bookToDelete == null)
                return NotFound();

            _unitOfWork.Books.Remove(bookToDelete);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        [HttpGet("getauthors/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAuthors(int id)
        {
            var book = await _unitOfWork.Books.GetFirstOrDefaultAsync(x => x.Id == id, new[] { "Genre" });

            if (book == null)
                return NotFound();

            var authors = await _unitOfWork.AuthorBook.GetAuthorsAsync(id);

            return Ok(authors);
        }

    }
}
