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
            var response = await _client.PostAsJsonAsync("Employees/create", emp);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }

        }

        public async Task<string> Login(LoginVM vm)
        {
            var response = await _client.PostAsJsonAsync("employee/login", vm);

            if (response.IsSuccessStatusCode)
            {
                var responseStream = response.Content.ReadAsStringAsync().Result;
                return responseStream;
            }

            throw new Exception(response.Content.ReadAsStringAsync().Result);
        }
    }
}
