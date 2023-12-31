﻿using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;
using LibraryApp.Models.ViewModels;

namespace LibraryApp.DataAccess.AuthenticationRepository.IAuthenticationRepository;

public interface IClientAuthentication : IAuthRepository
{
    Task<Clients> Register(ClientsDto dto);
    Task<string> Login(LoginVM loginUser);
    Task<bool> ClientExists(string email);
    Task ChangePassword(PasswordVM vm);

}
