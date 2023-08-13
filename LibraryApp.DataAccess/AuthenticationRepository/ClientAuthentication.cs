using LibraryApp.DataAccess.AuthenticationRepository.IAuthenticationRepository;
using LibraryApp.DataAccess.Repository.IRepository;
using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace LibraryApp.DataAccess.AuthenticationRepository;

public class ClientAuthentication : IClientAuthentication
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

    public async Task<string> Login(string email, string password)
    {
        var client = await _unitOfWork.Clients.GetFirstOrDefaultAsync(x => x.Email == email, new string[] { "Roles" });

        if (client == null)
            throw new Exception("User not found");

        if (!VerifyPassword(password, client.PasswordHash, client.PasswordSalt))
            throw new Exception("Incorrect Password!");

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

    private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
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

    private string CreateToken(Clients client)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.UserData, client.Id.ToString()),
            new Claim(ClaimTypes.Email, client.Email),
            new Claim(ClaimTypes.Role, client.Roles.Role),
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
