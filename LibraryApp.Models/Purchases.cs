using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace LibraryApp.Models;
public class Purchases
{

    [Key, Column(Order = 0)]

    public int ClientId { get; set; }
    public Clients Client { get; set; }

    [Key, Column(Order = 1)]
    public int BookId { get; set; }
    public Books Book { get; set; }

    [AllowNull]
    public int EmployeeId { get; set; }
    [AllowNull]
    public Employees Employee { get; set; }

    [Required]
    public int BuyPrice { get; set; }

    public DateTime PurchaseDate { get; set; } = DateTime.Now;

    public int Quantity { get; set; }

}

