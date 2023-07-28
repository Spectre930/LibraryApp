using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace LibraryApp.Models;

public class Clients
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string F_Name { get; set; }

    [Required]
    public string L_Name { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public DateTime DOB { get; set; }

    [Required]
    public int Age { get; set; }

    [Range(0, 5)]
    public int NumberOfBorrowed { get; set; }

    [AllowNull]
    public int RolesId { get; set; }
    [AllowNull]
    public Roles Roles { get; set; }
}
