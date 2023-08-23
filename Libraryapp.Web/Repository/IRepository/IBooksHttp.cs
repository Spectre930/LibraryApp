using LibraryApp.Models.Models;
using LibraryApp.Models.ViewModels;

namespace LibraryApp.Web.Repository.IRepository;

public interface IBooksHttp : IRepositoryHttp<Books>
{
    Task<IEnumerable<BooksIndexVM>> GetAllBooks();
    Task<BooksIndexVM> GetBook(int id);

    Task<IEnumerable<String>> GetAuthrosOfBook(int id);

    BooksVM ViewCreateBookVM(IEnumerable<Genres> genres, IEnumerable<Authors> authors);

    Task PostCreatedBook(BooksVM bookVM, List<int> selectedOptions);

    Task<BooksEditVM> GetEditBook(int id, IEnumerable<Genres> genres);

    Task UpdateBookAsync(BooksEditVM editVM);

    Task BorrowBook(int boookId);
    Task ReturnBook(int borrowId);

    Task PurchaseBook(PurchaseVM purchaseVM);

}
