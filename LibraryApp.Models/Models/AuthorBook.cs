using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Models.Models;

public class AuthorBook
{
    [Key, Column(Order = 0)]
    [Required]
    public int BookId { get; set; }
    public Books Book { get; set; }


    [Key, Column(Order = 1)]
    [Required]
    public int AuthorId { get; set; }
    public Authors Author { get; set; }


}
