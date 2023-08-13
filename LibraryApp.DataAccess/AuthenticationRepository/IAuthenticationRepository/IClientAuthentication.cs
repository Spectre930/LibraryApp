using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;


namespace LibraryApp.DataAccess.AuthenticationRepository.IAuthenticationRepository;

public interface IClientAuthentication
{
    Task<Clients> Register(ClientsDto dto);
    Task<string> Login(string email, string password);
    Task<bool> ClientExists(string email);
   
}
