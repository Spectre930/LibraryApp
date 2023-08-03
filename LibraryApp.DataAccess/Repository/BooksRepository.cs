using LibraryApp.DataAccess.Repository.IRepository;
using LibraryApp.Models.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;


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
            int oldCopies = obj.Copies;
            obj.Title = book.Title;
            obj.Description = book.Description;
            obj.Copies = book.Copies;
            obj.AvailableCopies = SetAvailableCopies(oldCopies, book.Copies, book.AvailableCopies);
            obj.AuthPrice = book.AuthPrice;
            obj.ListedPrice = SetBookPrice(book.AuthPrice, book.ListedPrice);

        }
        _db.Books.Update(obj);

    }

    public int SetAvailableCopies(int oldCopies, int newCopies, int availableCopies)
    {
        int dif = newCopies - oldCopies;
        return availableCopies + dif;
    }

    public int SetBookPrice(int authprice, int Listedprice)
    {
        if (Listedprice == null || Listedprice < authprice)
        {
            Listedprice = authprice + (authprice * 20 / 100);
            return Listedprice;
        }

        return Listedprice;


    }
}
