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
    public class RolesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public RolesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IEnumerable<Roles>> GetAll()
        {
            return await _unitOfWork.Roles.GetAllAsync();
        }

        [HttpGet("id")]
        [ProducesResponseType(typeof(Roles), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var Role = await _unitOfWork.Roles.GetFirstOrDefaultAsync(x => x.Id == id);
            if (Role == null)
                return NotFound();

            return Ok(Role);
        }

        [HttpPost]
        [Route("create")]
        [ProducesResponseType(typeof(Roles), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(Roles role)
        {
            await _unitOfWork.Roles.AddAsync(role);
            await _unitOfWork.SaveAsync();

            return Ok(role);
        }

        [HttpPut]
        [Route("{id}/update")]
        [ProducesResponseType(typeof(Roles), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id)
        {

            var role = await _unitOfWork.Roles
                                        .GetFirstOrDefaultAsync(x => x.Id == id);
            if (role == null)
                return BadRequest();

            _unitOfWork.Roles.Update(role);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}/delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var RoleToDelete = await _unitOfWork.Roles
                                        .GetFirstOrDefaultAsync(x => x.Id == id);

            if (RoleToDelete == null)
                return NotFound();

            _unitOfWork.Roles.Remove(RoleToDelete);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

    }
}
