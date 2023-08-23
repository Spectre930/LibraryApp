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
            TempData["success"] = "Membership Registered, Please Login!";
            return RedirectToAction("Login");
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
            await _unitOfWorkHttp.Clients.ClientLogin(userLogin);
            TempData["success"] = "Logged in Seccessfully!";
            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {

            ViewBag.Message = ex.Message;
            return View();
        }
    }

    public async Task<IActionResult> Borrows()
    {
        var ResObject = await _unitOfWorkHttp.Clients.GetBorrows();

        return View(ResObject);

    }

    [HttpPost]
    public async Task<IActionResult> Return(int id)
    {
        try
        {
            await _unitOfWorkHttp.Books.ReturnBook(id);
            return RedirectToAction("Borrows");
        }
        catch (Exception ex)
        {
            TempData["error"] = ex.Message;
            return RedirectToAction("Borrows");

        }
    }

    public async Task<IActionResult> Purchases()
    {
        var ResObject = await _unitOfWorkHttp.Clients.GetPurchases();

        return View(ResObject);

    }

}
