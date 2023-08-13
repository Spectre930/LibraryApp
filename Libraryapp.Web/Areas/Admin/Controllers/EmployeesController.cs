using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;
using LibraryApp.Models.ViewModels;
using LibraryApp.Web.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;



namespace LibraryApp.Web.Areas.Admin.Controllers;

public class EmployeesController : Controller
{

    private readonly IUnitOfWorkHttp _UnitOfWorkHttp;
    public EmployeesController(IUnitOfWorkHttp UnitOfWorkHttp)
    {
        _UnitOfWorkHttp = UnitOfWorkHttp;
    }
    public async Task<IActionResult> Index()
    {

        var ResObject = await _UnitOfWorkHttp.Employees.GetAllAsync("Employees");

        return View(ResObject);
    }

    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EmployeesDto emp)
    {
        await _UnitOfWorkHttp.Employees.CreateEmployee(emp);
        TempData["success"] = "Employee Created Successfully";
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var ResObject = await _UnitOfWorkHttp.Employees.GetAsync("Employees", id);
        return View(ResObject);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Employees employee)
    {
        await _UnitOfWorkHttp.Employees.UpdatePostAsync("Employees", employee, employee.Id);
        TempData["success"] = "Employee Updated Successfully";
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        var ResObject = await _UnitOfWorkHttp.Employees.GetAsync("Employees", id);
        return View(ResObject);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePOST(int id)
    {
        await _UnitOfWorkHttp.Employees.DeleteAsync("Employees", id);
        TempData["success"] = "Employee Deleted Successfully";
        return RedirectToAction("Index");
    }
}
