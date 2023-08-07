using LibraryApp.Models.Models;


namespace LibraryApp.Models.ViewModels;

public class BooksIndexVM
{
    public Books book { get; set; }
    public IEnumerable<String> authors { get; set; }
}
