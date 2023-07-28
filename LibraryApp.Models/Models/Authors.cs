using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Models.Models;

public class Authors
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
}
