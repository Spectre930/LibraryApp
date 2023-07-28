
namespace LibraryApp.Models.DTO;

public class BorrowDto
{

    public int BookId { get; set; }
    public int ClientId { get; set; }
    public DateTime BorrowDate { get; set; } = DateTime.Now;
    public DateTime ReturnDate { get; set; } = DateTime.Now.AddDays(14);

}
