using LibraryApp.DataAccess;
using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly LibraryContext _db;

        public EmployeesController(LibraryContext db) {
            _db = db;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IEnumerable<Employees>> GetAll() {
            var empList = await _db.Employees
                                   .Include(x => x.Role)
                                   .ToListAsync();

            return empList;
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(Employees), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id) {
            var emp = await _db.Employees
                               .Include(x => x.Role.Role)
                               .FirstOrDefaultAsync(x => x.Id == id);
            if (emp == null)
                return NotFound();


            return Ok(emp);
        }

        [HttpPost]
        [Route("create")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(EmployeesDto emp) {

            var employee = new Employees
            {
                F_Name = emp.F_Name,
                L_Name = emp.L_Name,
                Email = emp.Email,
                DOB = emp.DOB,
                Age = emp.Age,
                PhoneNumber = emp.PhoneNumber,
                TotalSales = 0,
                RoleId = emp.RoleId
            };

            await _db.Employees.AddAsync(employee);
            await _db.SaveChangesAsync();

            return Ok("Created");
        }

        [HttpPut]
        [Route("{id}/update")]
        [ProducesResponseType(typeof(Employees), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id) {
            var employee = await _db.Employees.Include(x => x.Role).FirstOrDefaultAsync(x => x.Id == id);
            if (employee == null)
                return BadRequest();

            _db.Employees.Update(employee);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}/delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id) {
            var EmployeeToDelete = await _db.Employees.FindAsync(id);

            if (EmployeeToDelete == null)
                return NotFound();

            _db.Employees.Remove(EmployeeToDelete);
            await _db.SaveChangesAsync();


            return NoContent();
        }

    }
}
