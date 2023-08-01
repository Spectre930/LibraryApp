using LibraryApp.DataAccess.Repository.IRepository.IPeople;
using LibraryApp.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Repository.IRepository;

public interface IUnitOfWork
{
    IRepository<Authors> Authors { get; }
    IRepository<Borrow> Borrows { get; }
    IRepository<Genres> Genres { get; }
    IRepository<Roles> Roles { get; }
    IRepository<AuthorBook> AuthorBook { get; }
    IBooksRepository Books { get; }
    IEmployeesRepository Employees { get; }
    IClientsRepository Clients { get; }
    IPurchasesRepository Purchases { get; }
    Task SaveAsync();
}
