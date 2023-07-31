using LibraryApp.DataAccess.Repository.IRepository;
using LibraryApp.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryContext _db;
        public IRepository<Authors> Authors { get; private set; }
        public IRepository<Books> Books { get; private set; }
        public IRepository<Borrow> Borrows { get; private set; }
        public IRepository<Clients> Clients { get; private set; }
        public IRepository<Employees> Employees { get; private set; }
        public IRepository<Genres> Genres { get; private set; }
        public IRepository<Roles> Roles { get; private set; }
        public IRepository<AuthorBook> AuthorBook { get; private set; }
        public IPurchasesRepository Purchases { get; private set; }

        public UnitOfWork(LibraryContext db)
        {
            _db = db;
            Authors = new Repository<Authors>(_db);
            Books = new Repository<Books>(_db);
            Borrows = new Repository<Borrow>(_db);
            Clients = new Repository<Clients>(_db);
            Employees = new Repository<Employees>(_db);
            Genres = new Repository<Genres>(_db);
            Roles = new Repository<Roles>(_db);
            AuthorBook = new Repository<AuthorBook>(_db);
            Purchases = new PurchasesRepository(_db);
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
