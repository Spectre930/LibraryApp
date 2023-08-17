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
        try
        {
            await _UnitOfWorkHttp.Employees.CreateEmployee(emp);
            TempData["success"] = "Employee Created Successfully";
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
        var ResObject = await _UnitOfWorkHttp.Employees.GetAsync("Employees", id);
        return View(ResObject);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Employees employee)
    {
        try
        {
            await _UnitOfWorkHttp.Employees.UpdatePostAsync("Employees", employee, employee.Id);
            TempData["success"] = "Employee Updated Successfully";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            //if (ex is HttpRequestException)
            ViewBag.Message = ex.Message;
            return View();
        }

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
        try
        {
            await _UnitOfWorkHttp.Employees.DeleteAsync("Employees", id);
            TempData["success"] = "Employee Deleted Successfully";
            return RedirectToAction("Index");

        }
        catch (Exception ex)
        {
            //if (ex is HttpRequestException)
            ViewBag.Message = ex.Message;
            return View();
        }
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginVM vm)
    {
        try
        {
            await _UnitOfWorkHttp.Employees.Login(vm);
            return RedirectToAction("Index", "EmpHome", new { area = "Admin" });
        }
        catch (Exception ex)
        {
            if (ex is HttpRequestException)
                ViewBag.Message = ex.Message;
            return View();
        }
    }
}
