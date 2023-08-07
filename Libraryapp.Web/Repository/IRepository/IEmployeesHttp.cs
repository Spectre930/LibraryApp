﻿using LibraryApp.Models.Models;
using LibraryApp.Models.ViewModels;

namespace LibraryApp.Web.Repository.IRepository
{
    public interface IEmployeesHttp : IRepositoryHttp<Employees>
    {
        Task CreateEmployee(EmployeesVM emp);
    }
}
