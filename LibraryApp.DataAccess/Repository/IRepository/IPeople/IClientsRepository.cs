using LibraryApp.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Repository.IRepository.IPeople
{
    public interface IClientsRepository : IRepository<Clients>
    {
        Task UpdateClient(Clients client);

    }
}
