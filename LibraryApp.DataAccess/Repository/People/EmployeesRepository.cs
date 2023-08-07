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
    public class EmployeesRepository : Repository<Employees>, IEmployeesRepository
    {
        private LibraryContext _db;

        public EmployeesRepository(LibraryContext db) : base(db)
        {
            _db = db;
        }

        public async Task MakeAdmin(int id)
        {
            var obj = await _db.Employees.FirstOrDefaultAsync(x => x.Id == id);
            obj.RoleId = 1;
            UpdateEmployee(obj);
        }

        public async Task RemoveAdmin(int id)
        {
            var obj = await _db.Employees.FirstOrDefaultAsync(x => x.Id == id);
            obj.RoleId = null;
            UpdateEmployee(obj);
        }

        public async Task UpdateEmployee(Employees employee)
        {
            var obj = await _db.Employees.FirstOrDefaultAsync(x => x.Id == employee.Id);

            if (obj != null)
            {
                if (obj.RoleId != employee.RoleId)
                {
                    obj.RoleId= employee.RoleId;
                    obj.Role = await _db.Roles.FindAsync(employee.RoleId);
                }
                obj.F_Name = employee.F_Name;
                obj.L_Name = employee.L_Name;
                obj.Email = employee.Email;
                obj.DOB = employee.DOB;
                obj.Age = base.SetAge(employee.DOB);
                obj.PhoneNumber = employee.PhoneNumber;
                obj.TotalSales = employee.TotalSales;

            }
            _db.Employees.Update(obj);

        }
    }
}
