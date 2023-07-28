using LibraryApp.DataAccess;
using LibraryApp.Models;
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

        public EmployeesController(LibraryContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IEnumerable<Employees>> GetAll()
        {
            var empList = await _db.Employees.ToListAsync();

            foreach (var emp in empList)
            {
                emp.Roles = await _db.Roles.FindAsync(emp.RolesId);
            }
            return empList;
        }

        [HttpGet("id")]
        [ProducesResponseType(typeof(Employees), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var emp = await _db.Employees.FindAsync(id);
            if (emp == null)
                return NotFound();

            emp.Roles = await _db.Roles.FindAsync(emp.RolesId);
            return Ok(emp);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Employees employee)
        {

            var emp = employee;
            emp.Roles = await _db.Roles.FindAsync(employee.RolesId);
            await _db.Employees.AddAsync(emp);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = emp.Id }, emp);
        }

        [HttpPut("id")]
        [ProducesResponseType(typeof(Employees), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, Employees employee)
        {
            if (id != employee.Id)
                return BadRequest();

            _db.Entry(employee).State = EntityState.Modified;

            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var EmployeeToDelete = await _db.Employees.FindAsync(id);

            if (EmployeeToDelete == null)
                return NotFound();

            _db.Employees.Remove(EmployeeToDelete);
            await _db.SaveChangesAsync();


            return NoContent();
        }

    }
}
