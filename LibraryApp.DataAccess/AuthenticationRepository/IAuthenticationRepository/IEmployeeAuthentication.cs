﻿using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;
using LibraryApp.Models.ViewModels;

namespace LibraryApp.DataAccess.AuthenticationRepository.IAuthenticationRepository;

public interface IEmployeeAuthentication : IAuthRepository
{
    Task<Employees> Register(EmployeesDto dto);
    Task<string> Login(LoginVM empLogin);
    Task<bool> EmployeeExists(string email);
    Task ChangePassword(PasswordVM vm);
}
