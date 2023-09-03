namespace LibraryApp.DataAccess.AuthenticationRepository.IAuthenticationRepository;

public interface IAuthRepository
{
    bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt);
    void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);

}
