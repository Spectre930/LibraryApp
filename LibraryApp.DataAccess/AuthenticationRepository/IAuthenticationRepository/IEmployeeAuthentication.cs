using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;


namespace LibraryApp.DataAccess.AuthenticationRepository.IAuthenticationRepository;

public interface IEmployeeAuthentication
{
    Task<Employees> Register(EmployeesDto dto);
    Task<string> Login(string email, string password);
    Task<bool> EmployeeExists(string email);
}
