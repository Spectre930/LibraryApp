using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;

namespace LibraryApp.Web.Repository.IRepository;

public interface IClientsHttp : IRepositoryHttp<Clients>
{
    Task CreateClient(ClientsDto client);
    Task UpdateClientAsync(Clients client);

}
