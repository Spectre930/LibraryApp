using LibraryApp.DataAccess.AuthenticationRepository.IAuthenticationRepository;
using LibraryApp.DataAccess.Repository.IRepository;
using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;
using LibraryApp.Models.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace LibraryApp.DataAccess.AuthenticationRepository;

public class EmployeeAuthentication : AuthRepository, IEmployeeAuthentication
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

    public async Task<string> Login(LoginVM empLogin)
    {
        var employee = await _unitOfWork.Employees.GetFirstOrDefaultAsync(x => x.Email == empLogin.email, new string[] { "Role" });

        if (employee == null || !VerifyPassword(empLogin.password, employee.PasswordHash, employee.PasswordSalt))
            throw new Exception("incorrect email or password");

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

    public async Task ChangePassword(PasswordVM vm)
    {
        var user = await _unitOfWork.Employees.GetFirstOrDefaultAsync(x => x.Id == vm.userId);

        if (VerifyPassword(vm.OldPassword, user.PasswordHash, user.PasswordSalt))
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(vm.NewPassword, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await _unitOfWork.SaveAsync();
        }
        else
        {
            throw new Exception("Incorrect Password!");
        }
    }
    private string CreateToken(Employees employee)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, employee.Id.ToString()),
            new Claim(ClaimTypes.Email, employee.Email),
            new Claim(ClaimTypes.Role, employee.Role.Role),
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
            _configuration.GetSection("JWT:Token").Value!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }


}
