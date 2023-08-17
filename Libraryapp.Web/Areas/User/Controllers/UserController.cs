using LibraryApp.Models.DTO;
using LibraryApp.Models.ViewModels;
using LibraryApp.Web.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Web.Areas.User.Controllers;

public class UserController : Controller
{
    private readonly IUnitOfWorkHttp _unitOfWorkHttp;

    public UserController(IUnitOfWorkHttp unitOfWorkHttp)
    {
        _unitOfWorkHttp = unitOfWorkHttp;
    }
    public IActionResult Index()
    {
        return View();
    }


    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register(ClientsDto clients)
    {
        try
        {
            await _unitOfWorkHttp.Clients.CreateClient(clients);
            TempData["success"] = "Membership Registered!";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            if (ex is HttpRequestException)
                ViewBag.Message = ex.Message;
            return View();

        }
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginVM userLogin)
    {
        try
        {
            var token = await _unitOfWorkHttp.Clients.ClientLogin(userLogin);

            return View(token);
        }
        catch (Exception ex)
        {

            ViewBag.Message = ex.Message;
            return View();
        }
    }

}
