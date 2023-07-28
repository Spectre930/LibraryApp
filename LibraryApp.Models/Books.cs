using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace LibraryApp.Models;

public class Books
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    [AllowNull]
    public int GenreId { get; set; }
    [AllowNull]
    public Genres Genre { get; set; }

    public int Copies { get; set; }

    public int AvailableCopies { get; set; }

    [Required]
    public int AuthPrice { get; set; }

    public string Author { get; set; }
}
