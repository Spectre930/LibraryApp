using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;
using LibraryApp.Models.ViewModels;
using LibraryApp.Web.Repository.IRepository;
using Newtonsoft.Json;

namespace LibraryApp.Web.Repository;

public class EmployeesHttp : RepositoryHttp<Employees>, IEmployeesHttp
{
    private readonly HttpClient _client;
    //private readonly IHttpContextAccessor _contextAccessor;
    public EmployeesHttp(HttpClient client, IHttpContextAccessor contextAccessor) : base(client, contextAccessor)
    {
        _client = client;
        //_contextAccessor = contextAccessor;
    }

    public async Task CreateEmployee(EmployeesDto emp)
    {
        AuthorizeHeader();
        var response = await _client.PostAsJsonAsync("Employees/create", emp);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response.Content.ReadAsStringAsync().Result);
        }

    }

    public async Task<bool> Login(LoginVM vm)
    {
        var response = await _client.PostAsJsonAsync("employee/login", vm);

        if (response.IsSuccessStatusCode)
        {
            await StoreToken(response);
            return true;

        }

        throw new Exception(response.Content.ReadAsStringAsync().Result);
    }

    public async Task ChangePassword(PasswordVM vm)
    {
        var response = await _client.PostAsJsonAsync($"employee/{vm.userId}/changepassword", vm);
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        throw new Exception(response.Content.ReadAsStringAsync().Result);
    }

    public async Task<Employees> UserInfo()
    {
        AuthorizeHeader();
        var id = GetClaims().Id;
        var response = await _client.GetAsync($"Employees/{id}");
        if (response.IsSuccessStatusCode)
        {
            var responseStream = response.Content.ReadAsStringAsync().Result;
            var ResObject = JsonConvert.DeserializeObject<Employees>(responseStream);
            return ResObject;
        }
        throw new Exception(response.Content.ReadAsStringAsync().Result);

    }
}
