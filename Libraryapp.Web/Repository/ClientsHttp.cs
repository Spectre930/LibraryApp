using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;
using LibraryApp.Models.ViewModels;
using LibraryApp.Web.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using static System.Net.WebRequestMethods;

namespace LibraryApp.Web.Repository;

public class ClientsHttp : RepositoryHttp<Clients>, IClientsHttp
{
    private readonly HttpClient _client;
    private readonly IHttpContextAccessor _contextAccessor;
    public ClientsHttp(HttpClient client, IHttpContextAccessor contextAccessor) : base(client, contextAccessor)
    {
        _client = client;
        _contextAccessor = contextAccessor;
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
        var serializerSettings = new JsonSerializerSettings();
        serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        var obj = JsonConvert.SerializeObject(client, Formatting.Indented, serializerSettings);
        var request = new HttpRequestMessage(HttpMethod.Put, "Clients/update");
        request.Headers.Add("accept", "text/plain");
        var content = new StringContent(obj, null, "application/json");
        request.Content = content;
        var response = await _client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var httpclient = new HttpClient();


    }
}
