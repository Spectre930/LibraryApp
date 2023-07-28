using LibraryApp.Models;

namespace LibraryApi.Operations;

public interface IOperations
{
    void PurchaseBook(int bookId, int clientId, int EmpId);
    void BorrowBook(Borrow borrow);
    void ReturnBook(Borrow borrow);
}
