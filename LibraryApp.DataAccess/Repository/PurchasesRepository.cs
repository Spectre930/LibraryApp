using LibraryApp.DataAccess.Repository.IRepository;
using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Repository;

public class PurchasesRepository : Repository<Purchases>, IPurchasesRepository
{
    private readonly LibraryContext _db;

    public PurchasesRepository(LibraryContext db) : base(db)
    {
        _db = db;

    }

    public async Task<Purchases> CreatePurchase(PurchasesDto p)
    {

        var purchase = new Purchases
        {
            ClientId = p.ClientId,
            BookId = p.BookId,
            EmployeeId = p.EmployeeId,
            Quantity = p.Quantity,
        };

        var book = await _db.Books.FindAsync(p.BookId);

        purchase.TotalPrice = book.ListedPrice * p.Quantity;

        var emp = _db.Employees.FirstOrDefault(x => x.Id == p.EmployeeId);
        emp.TotalSales += purchase.TotalPrice;

        return purchase;
    }


}
