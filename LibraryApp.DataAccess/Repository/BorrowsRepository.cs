using LibraryApp.DataAccess.Repository.IRepository;
using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;


namespace LibraryApp.DataAccess.Repository
{
    public class BorrowsRepository : Repository<Borrow>, IBorrowsRepository
    {
        private readonly LibraryContext _db;
        public BorrowsRepository(LibraryContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Borrow> BorrowBook(BorrowDto borrowDto)
        {
            var client = await _db.Clients.FindAsync(borrowDto.ClientId);
            var book = await _db.Books.FindAsync(borrowDto.BookId);
            if (book == null)
            {
                throw new Exception("Book not available");
            }
            if (client == null || client.NumberOfBorrowed == 5)
            {
                throw new Exception("You have borrowed the maximum allowed books");
            }
            var borrow = new Borrow
            {
                BookId = borrowDto.BookId,
                ClientId = borrowDto.ClientId,
                BorrowDate = borrowDto.BorrowDate,
                ReturnDate = borrowDto.BorrowDate.AddDays(14),
            };
            client.NumberOfBorrowed += 1;
            book.AvailableCopies -= 1;

            return borrow;


        }

        public Borrow ReturnBook(Borrow borrow)
        {

            var diff = DateTime.Now - borrow.ReturnDate;
            if (diff.TotalDays > 0)
            {
                borrow.LateReturnFee = ((float)diff.TotalDays);
            }
            return borrow;


        }
    }
}
