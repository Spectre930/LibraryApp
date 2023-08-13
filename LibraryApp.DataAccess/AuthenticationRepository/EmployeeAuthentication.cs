using LibraryApp.DataAccess.AuthenticationRepository.IAuthenticationRepository;
using LibraryApp.DataAccess.Repository.IRepository;
using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace LibraryApp.DataAccess.AuthenticationRepository;

public class EmployeeAuthentication : IEmployeeAuthentication
{
    private readonly IUnitOfWork _unitOfWork;

    public IConfiguration _configuration;

    public EmployeeAuthentication(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    public async Task<bool> EmployeeExists(string email)
    {
        if (await _unitOfWork.Employees.GetFirstOrDefaultAsync(x => x.Email == email) != null)
            return true;

        return false;
    }

    public async Task<string> Login(string email, string password)
    {
        var employee = await _unitOfWork.Employees.GetFirstOrDefaultAsync(x => x.Email == email, new string[] { "Role" });

        if (employee == null)
            throw new Exception("Employee not found");

        if (!VerifyPassword(password, employee.PasswordHash, employee.PasswordSalt))
            throw new Exception("Incorrect Password!");
        var token = CreateToken(employee);

        return token;
    }

    public async Task<Employees> Register(EmployeesDto dto)
    {
        if (await EmployeeExists(dto.Email))
        {
            throw new Exception("Email is already taken.");
        }
        byte[] passwordHash, passwordSalt;
        CreatePasswordHash(dto.Password, out passwordHash, out passwordSalt);

        var employee = await _unitOfWork.Employees.CreateEmployee(dto);

        employee.PasswordHash = passwordHash;
        employee.PasswordSalt = passwordSalt;

        await _unitOfWork.Employees.AddAsync(employee);
        await _unitOfWork.SaveAsync();

        return employee;
    }

    private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)); // Create hash using password salt.
            for (int i = 0; i < computedHash.Length; i++)
            { // Loop through the byte array
                if (computedHash[i] != passwordHash[i]) return false; // if mismatch
            }
        }
        return true; //if no mismatches.
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    private string CreateToken(Employees employee)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.UserData, employee.Id.ToString()),
            new Claim(ClaimTypes.Email, employee.Email),
            new Claim(ClaimTypes.Role, employee.Role.Role),
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
            _configuration.GetSection("JWT:Token").Value));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }


}
