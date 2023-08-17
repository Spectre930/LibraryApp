﻿using LibraryApp.DataAccess.AuthenticationRepository.IAuthenticationRepository;
using LibraryApp.DataAccess.Repository.IRepository;
using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace LibraryApi.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize(Roles = "Admin")]
public class EmployeesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthUnitOfWork _authUnitOfWork;

    public EmployeesController(IUnitOfWork unitOfWork, IAuthUnitOfWork authUnitOfWork)
    {
        _unitOfWork = unitOfWork;
        _authUnitOfWork = authUnitOfWork;
    }

    [HttpGet]
    [Route("getall")]
    public async Task<IEnumerable<Employees>> GetAll()
    {

        return await _unitOfWork.Employees
                                .GetAllAsync(new[] { "Role" });

    }

    [HttpGet]
    [Route("id")]
    [ProducesResponseType(typeof(Employees), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var emp = await _unitOfWork.Employees
                                   .GetFirstOrDefaultAsync(x => x.Id == id, new[] { "Role" });
        if (emp == null)
            return NotFound();

        return Ok(emp);
    }

    [HttpPost]
    [Route("create")]
    [ProducesResponseType(typeof(Employees), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(EmployeesDto emp)
    {
        try
        {

            var employee = await _authUnitOfWork.Employee.Register(emp);

            return Ok(employee);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpPut]
    [Route("update/{id}")]
    [ProducesResponseType(typeof(Employees), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Employees employee)
    {
        if (await _unitOfWork.Employees.GetFirstOrDefaultAsync(i => i.Id == employee.Id) == null)
            return BadRequest();

        await _unitOfWork.Employees.UpdateEmployee(employee);
        await _unitOfWork.SaveAsync();

        return NoContent();
    }

    [HttpDelete]
    [Route("delete/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var EmployeeToDelete = await _unitOfWork.Employees
                                   .GetFirstOrDefaultAsync(x => x.Id == id, new[] { "Role" });
        if (EmployeeToDelete == null)
            return NotFound();

        _unitOfWork.Employees.Remove(EmployeeToDelete);
        await _unitOfWork.SaveAsync();


        return NoContent();
    }



}
