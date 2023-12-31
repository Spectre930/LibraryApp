﻿using LibraryApp.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Repository.IRepository;

public interface IBooksRepository : IRepository<Books>
{
    Task UpdateBooks(Books book);
    int SetBookPrice(int authprice, int Listedprice);
    int SetAvailableCopies(int oldCopies, int newCopies, int availableCopies);


}
