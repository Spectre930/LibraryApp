using LibraryApp.Models.Models;


namespace LibraryApp.DataAccess.Repository.IRepository;

public interface IBookAuthorRepository : IRepository<AuthorBook>
{
    Task AddAuthorBookAsync(string authorIds, int bookId);
    Task<IEnumerable<String>> GetAuthorsAsync(int bookId);
    void DeletAuthorBooksOfAuthor(int autorId);

    IEnumerable<Books> GetBooksOfAuthor(int authorId);
}
