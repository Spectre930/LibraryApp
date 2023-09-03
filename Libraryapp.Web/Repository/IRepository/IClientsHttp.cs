using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;
using LibraryApp.Models.ViewModels;

namespace LibraryApp.Web.Repository.IRepository;

public interface IClientsHttp : IRepositoryHttp<Clients>
{
    Task CreateClient(ClientsDto client);
    Task<bool> ClientLogin(LoginVM userLogin);
    Task UpdateClientAsync(Clients client);
    Task<IEnumerable<Borrow>> GetBorrows();
    Task<IEnumerable<Purchases>> GetPurchases();

    Task<Clients> UserInfo();

    Task ChangePassword(PasswordVM vm);



}
