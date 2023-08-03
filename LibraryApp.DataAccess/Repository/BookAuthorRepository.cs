using LibraryApp.DataAccess.Repository.IRepository;
using LibraryApp.Models.Models;


namespace LibraryApp.DataAccess.Repository;

public class BookAuthorRepository : Repository<AuthorBook>, IBookAuthorRepository
{
    private readonly LibraryContext _db;

    public BookAuthorRepository(LibraryContext db) : base(db)
    {
        _db = db;
    }

    public async Task AddAuthorBookAsync(string authorIds, int bookId)
    {
        var ids = Array.ConvertAll<string, int>(authorIds.Split(','), Convert.ToInt32);
        foreach (var id in ids)
        {
            await _db.AuthorBooks.AddAsync(new AuthorBook
            {
                BookId = bookId,
                AuthorId = id
            });

        }
        await _db.SaveChangesAsync();
    }
}

