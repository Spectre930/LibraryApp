using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;
using LibraryApp.Models.ViewModels;

namespace LibraryApp.Web.Repository.IRepository
{
    public interface IEmployeesHttp : IRepositoryHttp<Employees>
    {
        Task CreateEmployee(EmployeesDto emp);
        Task<bool> Login(LoginVM vm);
    }
}
