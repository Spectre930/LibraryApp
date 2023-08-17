using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;
using LibraryApp.Models.ViewModels;
using LibraryApp.Web.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;
using static System.Net.WebRequestMethods;

namespace LibraryApp.Web.Repository;

public class ClientsHttp : RepositoryHttp<Clients>, IClientsHttp
{
    private readonly HttpClient _client;

    public ClientsHttp(HttpClient client) : base(client)
    {
        _client = client;
    }

    public async Task<string> ClientLogin(LoginVM userLogin)
    {
        var response = await _client.PostAsJsonAsync("user/login", userLogin);

        if (response.IsSuccessStatusCode)
        {
            var responseStream = response.Content.ReadAsStringAsync().Result;
            return responseStream;
        }

        throw new Exception(response.Content.ReadAsStringAsync().Result);
    }

    public async Task CreateClient(ClientsDto client)
    {
        //client.RolesId = 2;
        var response = await _client.PostAsJsonAsync("Clients/create", client);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response.Content.ReadAsStringAsync().Result);
        }
    }

    public async Task UpdateClientAsync(Clients client)
    {
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
