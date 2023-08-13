using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.AuthenticationRepository.IAuthenticationRepository;

public interface IAuthUnitOfWork
{
    IClientAuthentication Clients { get; }
    IEmployeeAuthentication Employee { get; }
}
