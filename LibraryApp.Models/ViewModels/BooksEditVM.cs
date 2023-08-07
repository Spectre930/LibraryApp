using LibraryApp.Models.Models;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace LibraryApp.Models.ViewModels;

public class BooksEditVM
{
    public Books book { get; set; }
    public IEnumerable<SelectListItem> GenresList { get; set; }
}
