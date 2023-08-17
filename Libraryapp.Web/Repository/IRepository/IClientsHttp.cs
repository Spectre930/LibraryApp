using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;
using LibraryApp.Models.ViewModels;

namespace LibraryApp.Web.Repository.IRepository;

public interface IClientsHttp : IRepositoryHttp<Clients>
{
    Task CreateClient(ClientsDto client);
    Task<string> ClientLogin(LoginVM userLogin);
    Task UpdateClientAsync(Clients client);

}
