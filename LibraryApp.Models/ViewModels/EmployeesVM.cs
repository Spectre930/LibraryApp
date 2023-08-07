using LibraryApp.Models.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Models.ViewModels
{
    public class EmployeesVM
    {
        public EmployeesDto employee { get; set; }

        [ValidateNever]
        public bool Admin { get; set; }
    }
}
