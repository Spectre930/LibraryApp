using LibraryApp.Models.Models;
using LibraryApp.Web.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;



namespace LibraryApp.Web.Areas.Admin.Controllers;

public class AuthorsController : Controller
{

    private readonly IUnitOfWorkHttp _UnitOfWorkHttp;
    public AuthorsController(IUnitOfWorkHttp UnitOfWorkHttp)
    {
        _UnitOfWorkHttp = UnitOfWorkHttp;
    }
    public async Task<IActionResult> Index()
    {
     
        var ResObject = await _UnitOfWorkHttp.Authors.GetAllAsync("Authors");

        return View(ResObject);
    }

    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Authors author)
    {

       
        await _UnitOfWorkHttp.Authors.CreatePostAsync("Authors", author);
        TempData["success"] = "Author Created Successfully";
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
       
        var ResObject = await _UnitOfWorkHttp.Authors.GetAsync("Authors", id);

        return View(ResObject);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Authors author)
    {
      
        await _UnitOfWorkHttp.Authors.UpdatePostAsync("Authors", author, author.Id);
        TempData["success"] = "Author Updated Successfully";
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
    
        var ResObject = await _UnitOfWorkHttp.Authors.GetAsync("Authors", id);

        return View(ResObject);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePOST(int id)
    {
      
        await _UnitOfWorkHttp.Authors.DeleteAsync("Authors", id);
        TempData["success"] = "Author Deleted Successfully";
        return RedirectToAction("Index");
    }
}
