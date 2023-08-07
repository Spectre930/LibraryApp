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

        await _UnitOfWorkHttp.Clients.CreateClient(client);

        TempData["success"] = "Client Created Successfully";
        return RedirectToAction("Index");
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

        await _UnitOfWorkHttp.Clients.UpdateClientAsync(client);
        TempData["success"] = "Client Updated Successfully";
        return RedirectToAction("Index");
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
        await _UnitOfWorkHttp.Clients.DeleteAsync("Clients", id);
        TempData["success"] = "Client Deleted Successfully";
        return RedirectToAction("Index");
    }

}
