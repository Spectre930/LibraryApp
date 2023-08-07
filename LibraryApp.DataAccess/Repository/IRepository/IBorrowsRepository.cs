using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Repository.IRepository
{
    public interface IBorrowsRepository : IRepository<Borrow>
    {
        Task<Borrow> BorrowBook(BorrowDto borrowDto);
        Borrow ReturnBook(Borrow borrow);
    }
}
