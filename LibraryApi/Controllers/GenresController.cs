using LibraryApp.DataAccess;
using LibraryApp.DataAccess.Repository.IRepository;
using LibraryApp.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenresController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public GenresController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [Route("getall")]
    [ProducesResponseType(typeof(Genres), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IEnumerable<Genres>> GetAll()
    {

        return await _unitOfWork.Genres.GetAllAsync();
    }

    [HttpGet("id")]
    [ProducesResponseType(typeof(Genres), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {

        var genre = await _unitOfWork.Genres.GetFirstOrDefaultAsync(x => x.Id == id);

        if (genre == null)
            return NotFound();

        return Ok(genre);
    }

    [HttpPost]
    [Route("create")]
    [ProducesResponseType(typeof(Genres), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(Genres genre)
    {
        await _unitOfWork.Genres.AddAsync(genre);
        await _unitOfWork.SaveAsync();
        return Ok(genre);
    }

    [HttpPut]
    [Route("update/{id}")]
    [ProducesResponseType(typeof(Genres), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, Genres genre)
    {

        if (genre.Id != id)
            return BadRequest();

        _unitOfWork.Genres.Update(genre);
        await _unitOfWork.SaveAsync();

        return NoContent();
    }

    [HttpDelete]
    [Route("delete/{id}")]
    [ProducesResponseType(typeof(Genres), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var genreToDelete = await _unitOfWork.Genres
                                             .GetFirstOrDefaultAsync(x => x.Id == id);
        if (genreToDelete == null)
            return NotFound();

        _unitOfWork.Genres.Remove(genreToDelete);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}

