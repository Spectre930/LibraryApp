using LibraryApp.DataAccess;
using LibraryApp.DataAccess.Repository.IRepository;
using LibraryApp.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IEnumerable<Authors>> GetAll()
        {
            return await _unitOfWork.Authors.GetAllAsync();
        }

        [HttpGet("id")]
        [ProducesResponseType(typeof(Authors), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var author = await _unitOfWork.Authors.GetFirstOrDefaultAsync(x => x.Id == id);
            if (author == null)
                return NotFound();

            return Ok(author);
        }

        [HttpPost]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(Authors author)
        {
            await _unitOfWork.Authors.AddAsync(author);
            await _unitOfWork.SaveAsync();

            return Ok(author);
        }

        [HttpPut]
        [Route("{id}/update")]
        [ProducesResponseType(typeof(Authors), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, Authors author)
        {

            if (id != author.Id)
                return BadRequest();

            _unitOfWork.Authors.Update(author);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}/delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var authorToDelete = await _unitOfWork.Authors.GetFirstOrDefaultAsync(x => x.Id == id);

            if (authorToDelete == null)
                return NotFound();

            _unitOfWork.Authors.Remove(authorToDelete);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

    }
}
