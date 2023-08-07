using LibraryApp.Models.Models;
using LibraryApp.Web.Repository.IRepository;

namespace LibraryApp.Web.Repository;

public class UnitOfWorkHttp : IUnitOfWorkHttp, IDisposable
{
    private readonly HttpClient _client;

    public IBooksHttp Books { get; private set; }
    public IClientsHttp Clients { get; private set; }
    public IEmployeesHttp Employees { get; private set; }
    public IRepositoryHttp<Authors> Authors { get; private set; }
    public IRepositoryHttp<Genres> Genres { get; private set; }

    public UnitOfWorkHttp(HttpClient client)
    {
        _client = client;
        Books = new BooksHttp(_client);
        Authors = new RepositoryHttp<Authors>(_client);
        Clients = new ClientsHttp(_client);
        Genres = new RepositoryHttp<Genres>(_client);
        Employees = new EmployeesHttp(_client);
    }

    public void Dispose()
    {
        _client.Dispose();
    }
}
