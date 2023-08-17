using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;
using LibraryApp.Models.ViewModels;

namespace LibraryApp.DataAccess.AuthenticationRepository.IAuthenticationRepository;

public interface IClientAuthentication
{
    Task<Clients> Register(ClientsDto dto);
    Task<string> Login(LoginVM loginUser);
    Task<bool> ClientExists(string email);

}
