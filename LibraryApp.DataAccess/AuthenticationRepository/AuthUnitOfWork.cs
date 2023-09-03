using LibraryApp.DataAccess.AuthenticationRepository.IAuthenticationRepository;
using LibraryApp.DataAccess.Repository.IRepository;
using Microsoft.Extensions.Configuration;


namespace LibraryApp.DataAccess.AuthenticationRepository;

public class AuthUnitOfWork : IAuthUnitOfWork
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;
    public IClientAuthentication Clients { get; private set; }
    public IEmployeeAuthentication Employee { get; private set; }

    public AuthUnitOfWork(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
        Clients = new ClientAuthentication(_unitOfWork, _configuration);
        Employee = new EmployeeAuthentication(_unitOfWork, _configuration);
    }

}
