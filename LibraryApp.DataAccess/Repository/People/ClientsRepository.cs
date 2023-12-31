﻿using LibraryApp.DataAccess.Repository.IRepository.IPeople;
using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace LibraryApp.DataAccess.Repository.People;

public class ClientsRepository : Repository<Clients>, IClientsRepository
{
    private readonly LibraryContext _db;

    public ClientsRepository(LibraryContext db) : base(db)
    {
        _db = db;
    }

    public async Task<Clients> CreateClientAsync(ClientsDto dto)
    {
        var user = new Clients
        {
            F_Name = dto.F_Name,
            L_Name = dto.L_Name,
            Email = dto.Email,
            DOB = dto.DOB,
            Age = SetAge(dto.DOB)


        };
        user.Roles = await _db.Roles.FirstOrDefaultAsync(r => r.Role == "User");
        user.RolesId = user.Roles.Id;

        return user;
    }

    public void DeleteClientTransactions(int id)
    {
        _db.Authors.FromSqlRaw($"DeletAClient {id}");
    }

    public async Task<IEnumerable<Borrow>> GetBorrowsOfAClient(int id)
    {
        var borrows = await _db.Borrows
            .Where(b => b.ClientId == id)
            .Include(b => b.Book)
            .Include(b => b.Client)
            .ToListAsync();


        return borrows;
    }

    public async Task<IEnumerable<Purchases>> GetPurchasesOfAClient(int id)
    {
        var purchases = await _db.Purchases
           .Where(b => b.ClientId == id)
           .Include(b => b.Book)
           .Include(b => b.Client)
           .ToListAsync();

        return purchases;
    }

    public async Task UpdateClient(Clients client)
    {
        var obj = await _db.Clients.FirstOrDefaultAsync(x => x.Id == client.Id);

        if (obj != null)
        {
            if (obj.RolesId != client.RolesId)
            {
                obj.Roles = await _db.Roles.FirstOrDefaultAsync(r => r.Role == "User");
                obj.RolesId = obj.Roles.Id;
            }
            obj.F_Name = client.F_Name;
            obj.L_Name = client.L_Name;
            obj.Email = client.Email;
            obj.DOB = client.DOB;
            obj.Age = SetAge(client.DOB);

        }
        _db.Clients.Update(obj);

    }
}
