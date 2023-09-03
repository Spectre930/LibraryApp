using LibraryApp.Web.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Web.Areas.User.Controllers
{
    public class UserAuthors : Controller
    {
        private readonly IUnitOfWorkHttp _UnitOfWorkHttp;

        public UserAuthors(IUnitOfWorkHttp UnitOfWorkHttp)
        {
            _UnitOfWorkHttp = UnitOfWorkHttp;
        }
        public async Task<IActionResult> Index()
        {
            var ResObject = await _UnitOfWorkHttp.Authors.GetAllAsync("Authors");

            return View(ResObject);

        }

        public async Task<IActionResult> Books(int id)
        {
            try
            {
                var ResObject = await _UnitOfWorkHttp.Books.GetBooksOfAuthor(id);
                return View(ResObject);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Index");
            }

        }
    }
}
