using LibraryApp.DataAccess.AuthenticationRepository.IAuthenticationRepository;
using LibraryApp.DataAccess.Repository.IRepository;
using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;
using LibraryApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers;

[Route("api")]
[ApiController]
public class AuthController : ControllerBase
{

    private readonly IAuthUnitOfWork _authUnitOfWork;

    public AuthController(IAuthUnitOfWork authUnitOfWork)
    {
        _authUnitOfWork = authUnitOfWork;

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
    [AllowAnonymous]
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
    [AllowAnonymous]

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
    [AllowAnonymous]

    public async Task<string> Userlogin(LoginVM userLogin)
    {
        try
        {
            var ClientToken = await _authUnitOfWork.Clients.Login(userLogin);

            return ClientToken;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }


    }

    [HttpPost]
    [Route("user/{id}/changepassword")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> UserChangePassword(PasswordVM vm)
    {
        try
        {
            await _authUnitOfWork.Clients.ChangePassword(vm);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Route("employee/{id}/changepassword")]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<IActionResult> EmpChangePassword(PasswordVM vm)
    {
        try
        {
            await _authUnitOfWork.Employee.ChangePassword(vm);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
