using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace LibraryApp.Models.Models;

public class Borrow
{
    [Key]
    public int Id { get; set; } 
    
    [Required]
    public int BookId { get; set; }
    public Books Book { get; set; }

    
    [Required]
    public int ClientId { get; set; }
    public Clients Client { get; set; }

    public DateTime BorrowDate { get; set; } = DateTime.Now;

    public DateTime ReturnDate { get; set; } = DateTime.Now.AddDays(14);

    [AllowNull]
    public float LateReturnFee { get; set; }

}
