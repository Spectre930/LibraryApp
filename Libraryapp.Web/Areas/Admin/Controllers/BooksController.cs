using LibraryApp.Models.Models;
using LibraryApp.Models.ViewModels;
using LibraryApp.Web.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
namespace LibraryApp.Web.Areas.Admin.Controllers;

public class BooksController : Controller
{

    private readonly IUnitOfWorkHttp _UnitOfWorkHttp;
    public BooksController(IUnitOfWorkHttp UnitOfWorkHttp)
    {
        _UnitOfWorkHttp = UnitOfWorkHttp;
    }
    public async Task<IActionResult> Index()
    {


        var ResObject = await _UnitOfWorkHttp.Books.GetAllBooks();

        return View(ResObject);
    }

    public async Task<IActionResult> Create()
    {
        var GenreResObject = await _UnitOfWorkHttp.Genres.GetAllAsync("Genres");
        var AuthResObject = await _UnitOfWorkHttp.Authors.GetAllAsync("Authors");

        var booksVM = _UnitOfWorkHttp.Books.ViewCreateBookVM(GenreResObject, AuthResObject);
        return View(booksVM);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BooksVM bookVM, List<int> selectedOptions)
    {
        try
        {
            await _UnitOfWorkHttp.Books.PostCreatedBook(bookVM, selectedOptions);
            TempData["success"] = "Book Created Successfully";
            return RedirectToAction("Index");

        }
        catch (Exception ex)
        {
            if (ex is HttpRequestException)
                ViewBag.Message = ex.Message;
            return View();
        }

    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var genres = await _UnitOfWorkHttp.Genres.GetAllAsync("Genres");
        var ResObject = await _UnitOfWorkHttp.Books.GetEditBook(id, genres);
        return View(ResObject);

    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(BooksEditVM bookVM)
    {
        try
        {
            await _UnitOfWorkHttp.Books.UpdateBookAsync(bookVM);
            TempData["success"] = "Book Updated Successfully";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ViewBag.Message = "error occured while editing please try again!";
            return View();
        }
    }

    public async Task<IActionResult> Delete(int id)
    {
        var ResObject = await _UnitOfWorkHttp.Books.GetBook(id);

        return View(ResObject);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePOST(BooksIndexVM bookVM)
    {
        try
        {
            await _UnitOfWorkHttp.Books.DeleteAsync("Books", bookVM.book.Id);
            TempData["success"] = "Book Deleted Successfully";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ViewBag.Message = "error occured while deleting please try again!";
            return View();
        }

    }
}
