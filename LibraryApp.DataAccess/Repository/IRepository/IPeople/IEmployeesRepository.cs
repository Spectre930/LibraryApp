using LibraryApp.Models.DTO;
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
        Task<Employees> CreateEmployee(EmployeesDto emp);
        Task UpdateEmployee(Employees employee);
        Task MakeAdmin(int id);
        Task RemoveAdmin(int id);
    }
}
