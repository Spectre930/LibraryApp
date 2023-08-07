using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Models.ViewModels
{
    public class BooksVM
    {
        public BooksDTO book { get; set; }
        public IEnumerable<SelectListItem> GenresList { get; set; }
        public IEnumerable<Authors> AuthorsList { get; set; }
    }
}
