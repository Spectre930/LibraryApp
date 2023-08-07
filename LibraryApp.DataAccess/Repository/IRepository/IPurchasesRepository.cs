using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Repository.IRepository;

public interface IPurchasesRepository : IRepository<Purchases>
{
    Task<Purchases> CreatePurchase(PurchasesDto p);

}
