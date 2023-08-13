using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;
using LibraryApp.Models.ViewModels;
using LibraryApp.Web.Repository.IRepository;

namespace LibraryApp.Web.Repository
{
    public class EmployeesHttp : RepositoryHttp<Employees>, IEmployeesHttp
    {
        private readonly HttpClient _client;

        public EmployeesHttp(HttpClient client) : base(client)
        {
            _client = client;
        }

        public async Task CreateEmployee(EmployeesDto emp)
        {
            await _client.PostAsJsonAsync("Employees/create", emp);
        }
    }
}
