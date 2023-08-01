using LibraryApp.DataAccess.Repository.IRepository;
using LibraryApp.Models.Models;
using Microsoft.EntityFrameworkCore;


namespace LibraryApp.DataAccess.Repository;

public class BooksRepository : Repository<Books>, IBooksRepository
{
    private readonly LibraryContext _db;

    public BooksRepository(LibraryContext db) : base(db)
    {
        _db = db;
    }
    public async Task UpdateBooks(Books book)
    {
        var obj = await _db.Books.FirstOrDefaultAsync(x => x.Id == book.Id);

        if (obj != null)
        {
            if (obj.GenreId != book.GenreId)
            {
                obj.Genre = await _db.Genres.FindAsync(book.GenreId);
            }
            obj.Title = book.Title;
            obj.Description = book.Description;
            obj.Author = book.Author;
            obj.Copies = book.Copies;
            obj.AvailableCopies = book.AvailableCopies;
            obj.AuthPrice = book.AuthPrice;
            int price = book.AuthPrice;
            obj.ListedPrice = price + (price * 20 / 100);

        }
        _db.Books.Update(book);

    }
}
