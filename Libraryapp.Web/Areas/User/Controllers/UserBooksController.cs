using LibraryApp.Models.ViewModels;
using LibraryApp.Web.Repository;
using LibraryApp.Web.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Web.Areas.User.Controllers;

public class UserBooksController : Controller
{
    private readonly IUnitOfWorkHttp _unitOfWorkHttp;
    public UserBooksController(IUnitOfWorkHttp UnitOfWorkHttp)
    {
        _unitOfWorkHttp = UnitOfWorkHttp;
    }

    public async Task<IActionResult> Index()
    {
        var ResObject = await _unitOfWorkHttp.Books.GetAllBooks();

        return View(ResObject);

    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {

        var book = await _unitOfWorkHttp.Books.GetBook(id);

        return View(book);
    }

    [HttpPost]
    public async Task<IActionResult> Borrow(int bookId)
    {
        await _unitOfWorkHttp.Books.BorrowBook(bookId);
        TempData["success"] = $"Book Borrowed, You have till {DateTime.Now.AddDays(14).Date.ToString()}";
        return RedirectToAction("Index", "UserBooks", new { area = "User" });
    }



    [HttpGet]
    public async Task<IActionResult> Purchase(int id)
    {
        var book = await _unitOfWorkHttp.Books.GetBook(id);
        var mv = new PurchaseVM
        {
            book = book,
            quantity = 0,
        };
        return View(mv);
    }

    [HttpPost]
    public async Task<IActionResult> Purchase(PurchaseVM vm)
    {
        try
        {
            await _unitOfWorkHttp.Books.PurchaseBook(vm);
        TempData["success"] = $"Book Purchased Successfully";
            return RedirectToAction("Index", "UserBooks");
        }
        catch (Exception ex)
        {
            TempData["error"] = ex.Message;
            return View(vm);
        }
    }


}
