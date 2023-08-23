using LibraryApp.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Models.ViewModels;
public class PurchaseVM
{
    public BooksIndexVM book { get; set; }
    public int quantity { get; set; }

}

