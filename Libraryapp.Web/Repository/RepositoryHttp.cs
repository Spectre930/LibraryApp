using LibraryApp.Models.Models;
using LibraryApp.Web.Repository.IRepository;
using Newtonsoft.Json;

namespace LibraryApp.Web.Repository;


public class RepositoryHttp<T> : IRepositoryHttp<T> where T : class
{
    private readonly HttpClient _client;
    private readonly Uri ba = new Uri("https://localhost:44395/api/");
    public RepositoryHttp(HttpClient client)
    {
        _client = client;
        _client.BaseAddress = ba;
    }


    public async Task<IEnumerable<T>> GetAllAsync(string controller)
    {
        var response = await _client.GetAsync($"{controller}/getall");
        response.EnsureSuccessStatusCode();
        var responseStream = response.Content.ReadAsStringAsync().Result;

        var ResObject = JsonConvert.DeserializeObject<IEnumerable<T>>(responseStream);

        return ResObject;
    }

    public async Task<T> GetAsync(string controller, int id)
    {
        var response = await _client.GetAsync($"{controller}/id?id={id}");
        response.EnsureSuccessStatusCode();
        var responseStream = response.Content.ReadAsStringAsync().Result;

        var ResObject = JsonConvert.DeserializeObject<T>(responseStream);

        return ResObject;
    }

    public async Task CreatePostAsync(string controller, T entity)
    {
        await _client.PostAsJsonAsync($"{controller}/create", entity);
    }

    public async Task DeleteAsync(string controller, int id)
    {
        var req = new HttpRequestMessage(HttpMethod.Delete, $"{controller}/delete/{id}");
        await _client.SendAsync(req);
    }

    public async Task UpdatePostAsync(string controller, T entity, int entityId)
    {

        var res = _client.PutAsJsonAsync($"{controller}/update/{entityId}", entity).Result;
        if (res.IsSuccessStatusCode)
        {
            return;

        }
        else
        {
            throw new Exception("No Success");
        }
    }


}
