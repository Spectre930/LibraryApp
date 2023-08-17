

using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models.ViewModels;

public class LoginVM
{
    [Required]
    public string email { get; set; }
    [Required]
    public string password { get; set; }

}
