using Humanizer;
using LibraryApp.DataAccess.AuthenticationRepository.IAuthenticationRepository;
using LibraryApp.DataAccess.Repository.IRepository;
using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;
using LibraryApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers;

[Route("api")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthUnitOfWork _authUnitOfWork;

    public AuthController(IUnitOfWork unitOfWork, IAuthUnitOfWork authUnitOfWork)
    {
        _authUnitOfWork = authUnitOfWork;
        _unitOfWork = unitOfWork;
    }

    [HttpPost]
    [Route("employee/register")]
    [ProducesResponseType(typeof(Employees), StatusCodes.Status200OK)]
    public async Task<IActionResult> EmployeeRegister(EmployeesDto emp)
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

    [HttpPost]
    [Route("employee/login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> EmployeeLogin(LoginVM empLogin)
    {
        try
        {
            var employeeToken = await _authUnitOfWork.Employee.Login(empLogin);

            return Ok(employeeToken);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }


    }

    [HttpPost]
    [Route("user/register")]
    [ProducesResponseType(typeof(Clients), StatusCodes.Status200OK)]
    public async Task<IActionResult> UserRegister(ClientsDto dto)
    {
        try
        {
            var client = await _authUnitOfWork.Clients.Register(dto);

            return Ok(client);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }


    }

    [HttpPost]
    [Route("user/login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Userlogin(LoginVM userLogin)
    {
        try
        {
            var ClientToken = await _authUnitOfWork.Clients.Login(userLogin);

            return Ok(ClientToken);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }


    }
}
