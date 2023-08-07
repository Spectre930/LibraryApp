using LibraryApp.DataAccess.Repository.IRepository;
using LibraryApp.Models.Models;
using Microsoft.EntityFrameworkCore;

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

    public void DeletAuthorBooksOfAuthor(int autorId)
    {
        _db.AuthorBooks.FromSqlRaw($"DeletAuthor {autorId}");
    }

    public async Task<IEnumerable<String>> GetAuthorsAsync(int bookId)
    {

        string[] includes = { "Author" };
        var ab = await GetAllAsync(includes);
        var res = ab.Where(u =>
        u.BookId == bookId
        )
        .Select(i =>
        i.Author.F_Name + " " + i.Author.L_Name);

        return res;


    }
}

