using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;


namespace LibraryApp.DataAccess.Repository.IRepository
{
    public interface IBorrowsRepository : IRepository<Borrow>
    {
        Task<Borrow> BorrowBook(BorrowDto borrowDto);
        Borrow SetReturnFee(Borrow borrow);
        Task<Borrow> ReturnBook(Borrow borrow);
    }
}
