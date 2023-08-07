using LibraryApp.Models.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Models.ViewModels
{
    public class EmployessVM
    {
        public EmployeesDto employee { get; set; }

        public IEnumerable<SelectListItem> RolesList { get; set; }
    }
}
