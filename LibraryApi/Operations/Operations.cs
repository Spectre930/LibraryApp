using LibraryApp.Models.Models;

namespace LibraryApi.Operations
{
    public class Operations : IOperations
    {

        public void BorrowBook(Borrow borrow) {
            throw new NotImplementedException();
        }

        public void PurchaseBook(int bookId, int clientId, int EmpId) {
            throw new NotImplementedException();
        }

        public void ReturnBook(int bookId, int clientId) {
            throw new NotImplementedException();
        }

        public void ReturnBook(Borrow borrow) {
            throw new NotImplementedException();
        }
    }
}
