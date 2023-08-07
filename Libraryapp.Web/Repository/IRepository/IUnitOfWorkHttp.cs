using LibraryApp.Models.Models;

namespace LibraryApp.Web.Repository.IRepository;

public interface IUnitOfWorkHttp
{
    IBooksHttp Books { get; }
    IClientsHttp Clients { get; }
    IEmployeesHttp Employees { get; }
    IRepositoryHttp<Authors> Authors { get; }
    IRepositoryHttp<Genres> Genres { get; }
}
