﻿using LibraryApp.DataAccess.Repository.IRepository;
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
        [Route("id")]
        [ProducesResponseType(typeof(Clients), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var client = await _unitOfWork.Clients.GetFirstOrDefaultAsync(x => x.Id == id, new[] { "Roles" });

            if (client == null)
                return NotFound();

            return Ok(client);
        }

        [HttpGet]
        [Route("getborrows/id")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IEnumerable<Borrow>> GetBorrows(int id)
        {
            return await _unitOfWork.Clients.GetBorrowsOfAClient(id);


        }

        [HttpGet]
        [Route("getpurchases/id")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IEnumerable<Purchases>> GetPurchases(int id)
        {
            return await _unitOfWork.Clients.GetPurchasesOfAClient(id);


        }

        [HttpPost]
        [Route("create")]
        [ProducesResponseType(typeof(Clients), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(ClientsDto client)
        {


            var user = await _unitOfWork.Clients.CreateClientAsync(client);

            return Ok(user);
        }

        [HttpPut]
        [Route("update")]
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

            _unitOfWork.Clients.DeleteClientTransactions(id);
            _unitOfWork.Clients.Remove(ClientToDelete);
            await _unitOfWork.SaveAsync();


            return NoContent();
        }

    }
}
