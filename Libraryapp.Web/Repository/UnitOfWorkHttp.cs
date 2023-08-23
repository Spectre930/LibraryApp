using LibraryApp.Models.Models;
using LibraryApp.Web.Repository.IRepository;

namespace LibraryApp.Web.Repository;

public class UnitOfWorkHttp : IUnitOfWorkHttp, IDisposable
{
    private readonly HttpClient _client;
    private readonly IHttpContextAccessor _contextAccessor;
    public IBooksHttp Books { get; private set; }
    public IClientsHttp Clients { get; private set; }
    public IEmployeesHttp Employees { get; private set; }
    public IRepositoryHttp<Authors> Authors { get; private set; }
    public IRepositoryHttp<Genres> Genres { get; private set; }

    public UnitOfWorkHttp(HttpClient client, IHttpContextAccessor contextAccessor)
    {
        _client = client;
        _contextAccessor = contextAccessor;
        Books = new BooksHttp(_client, _contextAccessor);
        Authors = new RepositoryHttp<Authors>(_client, _contextAccessor);
        Clients = new ClientsHttp(_client, _contextAccessor);
        Genres = new RepositoryHttp<Genres>(_client, _contextAccessor);
        Employees = new EmployeesHttp(_client, _contextAccessor);
    }
    public void Logout()
    {
        if (_contextAccessor.HttpContext != null)
        {
            _contextAccessor.HttpContext.Session.Remove("Token");
            _client.DefaultRequestHeaders.Authorization = null;
            return;
        }
        throw new Exception("There was a problem Logging out");
    }
    public void Dispose()
    {
        _client.Dispose();
    }
}
