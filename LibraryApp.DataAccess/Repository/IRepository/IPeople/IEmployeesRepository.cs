using LibraryApp.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Repository.IRepository.IPeople
{
    public interface IEmployeesRepository : IRepository<Employees>
    {
        Task UpdateEmployee(Employees employee);
    }
}
