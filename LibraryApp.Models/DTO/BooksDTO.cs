
namespace LibraryApp.Models.DTO;

public class BooksDTO
{

    public string Title { get; set; }
    public string Description { get; set; }
    public int GenreId { get; set; }
    public int Copies { get; set; }
    public int AvailableCopies { get; set; }
    public int AuthPrice { get; set; }
    public string Author { get; set; }
}
