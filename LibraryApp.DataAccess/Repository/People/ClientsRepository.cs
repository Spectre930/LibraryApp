using LibraryApp.DataAccess.Repository.IRepository.IPeople;
using LibraryApp.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Repository.People
{
    public class ClientsRepository : Repository<Clients>, IClientsRepository
    {
        private readonly LibraryContext _db;

        public ClientsRepository(LibraryContext db) : base(db)
        {
            _db = db;
        }
        public async Task UpdateClient(Clients client)
        {
            var obj = await _db.Clients.FirstOrDefaultAsync(x => x.Id == client.Id);

            if (obj != null)
            {
                if (obj.RolesId != client.RolesId)
                {
                    obj.Roles = await _db.Roles.FindAsync(client.RolesId);
                }
                obj.F_Name = client.F_Name;
                obj.L_Name = client.L_Name;
                obj.Email = client.Email;
                obj.DOB = client.DOB;
                obj.Age = base.SetAge(client.DOB);

            }
            _db.Clients.Update(client);

        }
    }
}
