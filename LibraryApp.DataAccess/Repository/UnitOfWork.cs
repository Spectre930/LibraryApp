using LibraryApp.DataAccess.Repository.IRepository;
using LibraryApp.DataAccess.Repository.IRepository.IPeople;
using LibraryApp.DataAccess.Repository.People;
using LibraryApp.Models.Models;


namespace LibraryApp.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly LibraryContext _db;
    public IRepository<Authors> Authors { get; private set; }
    public IRepository<Borrow> Borrows { get; private set; }
    public IRepository<Genres> Genres { get; private set; }
    public IRepository<Roles> Roles { get; private set; }
    public IBookAuthorRepository AuthorBook { get; private set; }
    public IBooksRepository Books { get; private set; }
    public IEmployeesRepository Employees { get; private set; }
    public IClientsRepository Clients { get; private set; }
    public IPurchasesRepository Purchases { get; private set; }

    public UnitOfWork(LibraryContext db)
    {
        _db = db;
        Authors = new Repository<Authors>(_db);
        Borrows = new Repository<Borrow>(_db);
        Genres = new Repository<Genres>(_db);
        Roles = new Repository<Roles>(_db);
        AuthorBook = new BookAuthorRepository(_db);
        Clients = new ClientsRepository(_db);
        Books = new BooksRepository(_db);
        Employees = new EmployeesRepository(_db);
        Purchases = new PurchasesRepository(_db);
    }

    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
