using LibraryApp.DataAccess.Repository.IRepository;
using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Repository
{
    public class PurchasesRepository : Repository<Purchases>, IPurchasesRepository
    {
        private LibraryContext _db;

        public PurchasesRepository(LibraryContext db) : base(db)
        {
            _db = db;
        }

        public Purchases CreatePurchase(PurchasesDto p)
        {
            var purchase = new Purchases
            {
                ClientId = p.ClientId,
                BookId = p.BookId,
                EmployeeId = p.EmployeeId,
                Quantity = p.Quantity,
            };
            int price = purchase.Book.AuthPrice;
            purchase.BuyPrice = price + (price * 20 / 100);

            return purchase;
        }
    }
}
