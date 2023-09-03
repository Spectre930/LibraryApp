using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;
using LibraryApp.Models.ViewModels;
using LibraryApp.Web.Repository;
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
        if (_UnitOfWorkHttp.Employees.GetClaims().role == "Admin")
        {
            var ResObject = await _UnitOfWorkHttp.Employees.GetAllAsync("Employees");

            return View(ResObject);
        }
        return View("Forbidden");

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
            TempData["Role"] = _UnitOfWorkHttp.Employees.GetClaims().role;
            ViewBag.Role = _UnitOfWorkHttp.Employees.GetClaims().role;

            return RedirectToAction("Index", "EmpHome", new { area = "Admin" });
        }
        catch (Exception ex)
        {
            if (ex is HttpRequestException)
                ViewBag.Message = ex.Message;
            return View();
        }
    }

    [HttpGet]
    public IActionResult ChangePassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword(PasswordVM vm)
    {
        vm.userId = _UnitOfWorkHttp.Employees.GetClaims().Id;
        if (vm.NewPassword.Equals(vm.OldPassword))
        {
            ViewBag.Message = "the two passwords are the same!";
            return View();
        }
        try
        {
            await _UnitOfWorkHttp.Employees.ChangePassword(vm);
            TempData["success"] = "Password Changed Successfully!";
            return RedirectToAction("Info", "Employees");
        }
        catch (Exception ex)
        {
            ViewBag.Message = ex.Message;
            return View();
        }

    }

    public async Task<IActionResult> Info()
    {
        try
        {
            var info = await _UnitOfWorkHttp.Employees.UserInfo();
            return View(info);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = ex.Message;
            return View("Index", "Home");
        }
    }


    [HttpGet]
    public IActionResult Forbidden()
    {
        return View();
    }
}
