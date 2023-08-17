using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;
using LibraryApp.Web.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;



namespace LibraryApp.Web.Areas.Admin.Controllers;

public class ClientsController : Controller
{
    private readonly IUnitOfWorkHttp _UnitOfWorkHttp;
    public ClientsController(IUnitOfWorkHttp UnitOfWorkHttp)
    {
        _UnitOfWorkHttp = UnitOfWorkHttp;

    }
    public async Task<IActionResult> Index()
    {
        var ResObject = await _UnitOfWorkHttp.Clients.GetAllAsync("Clients");
        return View(ResObject);
    }

    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ClientsDto client)
    {
        try
        {
            await _UnitOfWorkHttp.Clients.CreateClient(client);

            TempData["success"] = "Client Created Successfully";
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


        var ResObject = await _UnitOfWorkHttp.Clients.GetAsync("Clients", id);

        return View(ResObject);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Clients client)
    {
        try
        {
            await _UnitOfWorkHttp.Clients.UpdateClientAsync(client);
            TempData["success"] = "Client Updated Successfully";
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
        var ResObject = await _UnitOfWorkHttp.Clients.GetAsync("Clients", id);

        return View(ResObject);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePOST(int id)
    {
        try
        {
            await _UnitOfWorkHttp.Clients.DeleteAsync("Clients", id);
            TempData["success"] = "Client Deleted Successfully";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {

            ViewBag.Message = "error occured while deleting please try again!";

            return View();
        }

    }

}
