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

public class ClientAuthentication : AuthRepository, IClientAuthentication
{
    private readonly IUnitOfWork _unitOfWork;

    public IConfiguration _configuration;

    public ClientAuthentication(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    public async Task<bool> ClientExists(string email)
    {
        if (await _unitOfWork.Clients.GetFirstOrDefaultAsync(x => x.Email == email) != null)
            return true;

        return false;
    }

    public async Task<string> Login(LoginVM loginUser)
    {
        var client = await _unitOfWork.Clients.GetFirstOrDefaultAsync(x => x.Email == loginUser.email, new string[] { "Roles" });

        if (client == null || !VerifyPassword(loginUser.password, client.PasswordHash, client.PasswordSalt))
            throw new Exception("incorrect email or password");



        var token = CreateToken(client);
        return token;
    }

    public async Task<Clients> Register(ClientsDto dto)
    {
        if (await ClientExists(dto.Email))
        {
            throw new Exception("Email is already taken.");
        }
        byte[] passwordHash, passwordSalt;
        CreatePasswordHash(dto.Password, out passwordHash, out passwordSalt);

        var client = await _unitOfWork.Clients.CreateClientAsync(dto);

        client.PasswordHash = passwordHash;
        client.PasswordSalt = passwordSalt;

        await _unitOfWork.Clients.AddAsync(client);
        await _unitOfWork.SaveAsync();

        return client;
    }

    public async Task ChangePassword(PasswordVM vm)
    {
        var user = await _unitOfWork.Clients.GetFirstOrDefaultAsync(x => x.Id == vm.userId);

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
    private string CreateToken(Clients client)
    {
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, client.Id.ToString()),
            new Claim(ClaimTypes.Email, client.Email),
        new Claim(ClaimTypes.Role, client.Roles.Role),
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
