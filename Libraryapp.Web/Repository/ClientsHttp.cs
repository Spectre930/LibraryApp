using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;
using LibraryApp.Models.ViewModels;
using LibraryApp.Web.Repository.IRepository;
using Newtonsoft.Json;


namespace LibraryApp.Web.Repository;

public class ClientsHttp : RepositoryHttp<Clients>, IClientsHttp
{
    private readonly HttpClient _client;
    //private readonly IHttpContextAccessor _contextAccessor;
    public ClientsHttp(HttpClient client, IHttpContextAccessor contextAccessor) : base(client, contextAccessor)
    {
        _client = client;
        //_contextAccessor = contextAccessor;
    }

    public async Task ChangePassword(PasswordVM vm)
    {
        var response = await _client.PostAsJsonAsync($"user/{vm.userId}/changepassword", vm);
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        throw new Exception(response.Content.ReadAsStringAsync().Result);
    }

    public async Task<bool> ClientLogin(LoginVM userLogin)
    {
        var response = await _client.PostAsJsonAsync("user/login", userLogin);

        if (response.IsSuccessStatusCode)
        {
            await StoreToken(response);
            return true;
        }

        throw new Exception(response.Content.ReadAsStringAsync().Result);
    }

    public async Task CreateClient(ClientsDto client)
    {
        AuthorizeHeader();
        var response = await _client.PostAsJsonAsync("Clients/create", client);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response.Content.ReadAsStringAsync().Result);
        }
    }

    public async Task<IEnumerable<Borrow>> GetBorrows()
    {
        AuthorizeHeader();
        var clientId = GetClaims().Id;
        var response = await _client.GetAsync($"Clients/{clientId}/borrows");
        if (response.IsSuccessStatusCode)
        {
            var responseStream = response.Content.ReadAsStringAsync().Result;
            var ResObject = JsonConvert.DeserializeObject<IEnumerable<Borrow>>(responseStream);
            return ResObject;
        }
        throw new Exception(response.Content.ReadAsStringAsync().Result);
    }

    public async Task<IEnumerable<Purchases>> GetPurchases()
    {
        AuthorizeHeader();
        var clientId = GetClaims().Id;
        var response = await _client.GetAsync($"Clients/{clientId}/purchases");
        if (response.IsSuccessStatusCode)
        {
            var responseStream = response.Content.ReadAsStringAsync().Result;
            var ResObject = JsonConvert.DeserializeObject<IEnumerable<Purchases>>(responseStream);
            return ResObject;
        }
        throw new Exception(response.Content.ReadAsStringAsync().Result);
    }

    public async Task UpdateClientAsync(Clients client)
    {
        AuthorizeHeader();

        var res = await _client.PutAsJsonAsync($"Clients/update/{client.Id}", client);
        res.EnsureSuccessStatusCode();
    }

    public async Task<Clients> UserInfo()
    {
        AuthorizeHeader();
        var id = GetClaims().Id;
        var response = await _client.GetAsync($"Clients/{id}");
        if (response.IsSuccessStatusCode)
        {
            var responseStream = response.Content.ReadAsStringAsync().Result;
            var ResObject = JsonConvert.DeserializeObject<Clients>(responseStream);
            return ResObject;
        }
        throw new Exception(response.Content.ReadAsStringAsync().Result);

    }
}
