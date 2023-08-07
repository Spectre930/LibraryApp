using LibraryApp.Web.Repository.IRepository;
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


}
