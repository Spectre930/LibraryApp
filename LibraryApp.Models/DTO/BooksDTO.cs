
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models.DTO;

public class BooksDTO
{
    [Required]
    public string Title { get; set; }
    public string Description { get; set; }
    public int GenreId { get; set; }
    [Required]
    public int Copies { get; set; }
    public int AvailableCopies { get; set; }
    [Required]

    public int AuthPrice { get; set; }
    public int ListedPrice { get; set; }
    public string AuthorIds { get; set; }
}
