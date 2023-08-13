using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace LibraryApp.Models.Models;
public class Purchases
{

    [Key]
    public int Id { get; set; }
    [Required]
    public int ClientId { get; set; }
    public Clients Client { get; set; }
    [Required]
    public int BookId { get; set; }
    public Books Book { get; set; }

    [Required]
    [AllowNull]
    public int TotalPrice { get; set; }

    public DateTime PurchaseDate { get; set; } = DateTime.Now;

    public int Quantity { get; set; }

}

