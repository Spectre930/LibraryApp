using LibraryApp.Models.Models;
using LibraryApp.Web.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;



namespace LibraryApp.Web.Areas.Admin.Controllers;

public class GenresController : Controller
{
    private readonly IUnitOfWorkHttp _UnitOfWorkHttp;
    public GenresController(IUnitOfWorkHttp UnitOfWorkHttp)
    {
        _UnitOfWorkHttp = UnitOfWorkHttp;
    }
    public async Task<IActionResult> Index()
    {
        var ResObject = await _UnitOfWorkHttp.Genres.GetAllAsync("Genres");
        return View(ResObject);
    }

    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Genres genre)
    {
        try
        {
            await _UnitOfWorkHttp.Genres.CreatePostAsync("Genres", genre);
            TempData["success"] = "Genre Created Successfully";
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
        var ResObject = await _UnitOfWorkHttp.Genres.GetAsync("Genres", id);
        return View(ResObject);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Genres genre)
    {
        try
        {
            await _UnitOfWorkHttp.Genres.UpdatePostAsync("Genres", genre, genre.Id);
            TempData["success"] = "Genre Updated Successfully";
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
        var ResObject = await _UnitOfWorkHttp.Genres.GetAsync("Genres", id);
        return View(ResObject);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePOST(int id)
    {
        try
        {
            await _UnitOfWorkHttp.Genres.DeleteAsync("Genres", id);
            TempData["success"] = "Genre Deleted Successfully";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {

            ViewBag.Message = "error occured while deleting please try again!";
            return View();
        }

    }
}
