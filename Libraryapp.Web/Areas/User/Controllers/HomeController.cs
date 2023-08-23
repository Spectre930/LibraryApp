using LibraryApp.Models.Models;
using LibraryApp.Web.Repository;
using LibraryApp.Web.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LibraryApp.Web.Areas.User.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWorkHttp _unitOfWorkHttp;

        public HomeController(IUnitOfWorkHttp unitOfWorkHttp)
        {
            _unitOfWorkHttp = unitOfWorkHttp;
        }

        public IActionResult Index()
        {


            return View();
        }


        public IActionResult Logout()
        {
            try
            {
                _unitOfWorkHttp.Logout();
                TempData["success"] = "You have been successfully logged out.";
                ViewBag.status = null;
                return RedirectToAction("Login", "User", new { area = "User" });
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
            // Redirect to another page or login page


        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}