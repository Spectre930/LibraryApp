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
    IRepository<Books> Books { get; }
    IRepository<Borrow> Borrows { get; }
    IRepository<Clients> Clients { get; }
    IRepository<Employees> Employees { get; }
    IRepository<Genres> Genres { get; }
    IRepository<Roles> Roles { get; }
    IRepository<AuthorBook> AuthorBook { get; }
    IPurchasesRepository Purchases { get; }
    Task SaveAsync();
}
