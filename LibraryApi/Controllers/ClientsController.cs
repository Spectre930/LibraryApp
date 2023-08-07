using LibraryApp.DataAccess.Repository.IRepository;
using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;
using Microsoft.AspNetCore.Mvc;


namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClientsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IEnumerable<Clients>> GetAll()
        {

            return await _unitOfWork.Clients.GetAllAsync(new[] { "Roles" });
        }

        [HttpGet]
        [Route("get/id")]
        [ProducesResponseType(typeof(Clients), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var client = await _unitOfWork.Clients.GetFirstOrDefaultAsync(x => x.Id == id, new[] { "Roles" });

            if (client == null)
                return NotFound();

            return Ok(client);
        }

        [HttpPost]
        [Route("create")]
        [ProducesResponseType(typeof(Clients), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(ClientsDto client)
        {

            var user = new Clients
            {
                F_Name = client.F_Name,
                L_Name = client.L_Name,
                Email = client.Email,
                DOB = client.DOB,
                Age = _unitOfWork.Clients.SetAge(client.DOB),
                RolesId = client.RolesId
            };
            await _unitOfWork.Clients.AddAsync(user);
            await _unitOfWork.SaveAsync();

            return Ok(user);
        }

        [HttpPut]
        [Route("update/{id}")]
        [ProducesResponseType(typeof(Clients), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Clients client)
        {

            if (await _unitOfWork.Clients.GetFirstOrDefaultAsync(i => i.Id == client.Id) == null)
                return BadRequest();

            await _unitOfWork.Clients.UpdateClient(client);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var ClientToDelete = await _unitOfWork.Clients.GetFirstOrDefaultAsync(x => x.Id == id);

            if (ClientToDelete == null)
                return NotFound();


            _unitOfWork.Clients.Remove(ClientToDelete);
            await _unitOfWork.SaveAsync();


            return NoContent();
        }

    }
}
