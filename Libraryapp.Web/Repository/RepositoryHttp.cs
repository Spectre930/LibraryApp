using LibraryApp.Models.Models;
using LibraryApp.Web.Repository.IRepository;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace LibraryApp.Web.Repository;


public class RepositoryHttp<T> : IRepositoryHttp<T> where T : class
{
    private readonly HttpClient _client;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly Uri ba = new Uri("https://localhost:44395/api/");
    public RepositoryHttp(HttpClient client, IHttpContextAccessor contextAccessor)
    {
        _client = client;
        _contextAccessor = contextAccessor;
        _client.BaseAddress = ba;

    }


    public async Task<IEnumerable<T>> GetAllAsync(string controller)
    {
        AuthorizeHeader();
        var response = await _client.GetAsync($"{controller}/getall");
        response.EnsureSuccessStatusCode();
        var responseStream = response.Content.ReadAsStringAsync().Result;

        var ResObject = JsonConvert.DeserializeObject<IEnumerable<T>>(responseStream);

        return ResObject;
    }

    public async Task<T> GetAsync(string controller, int id)
    {
        AuthorizeHeader();
        var response = await _client.GetAsync($"{controller}/id?id={id}");
        response.EnsureSuccessStatusCode();
        var responseStream = response.Content.ReadAsStringAsync().Result;

        var ResObject = JsonConvert.DeserializeObject<T>(responseStream);

        return ResObject;
    }

    public async Task CreatePostAsync(string controller, T entity)
    {
        AuthorizeHeader();
        await _client.PostAsJsonAsync($"{controller}/create", entity);
    }

    public async Task DeleteAsync(string controller, int id)
    {
        AuthorizeHeader();
        var req = new HttpRequestMessage(HttpMethod.Delete, $"{controller}/delete/{id}");
        await _client.SendAsync(req);
    }

    public async Task UpdatePostAsync(string controller, T entity, int entityId)
    {
        AuthorizeHeader();
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

    public User GetClaims()
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        // Retrieve the JWT from the cookie
        var session = _contextAccessor.HttpContext.Session.GetString("Token");
        var token = JsonConvert.DeserializeObject<string>(session);
        var jwtToken = tokenHandler.ReadJwtToken(token);



        string role = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        int id = int.Parse(jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
        string email = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

        var user = new User(id, role, email);
        return user;
    }

    public async Task StoreToken(HttpResponseMessage response)
    {
        string res = await response.Content.ReadAsStringAsync();
        string token = JsonConvert.DeserializeObject<string>(res);


        _contextAccessor.HttpContext.Session.SetString("Token", JsonConvert.SerializeObject(token));


        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    public void AuthorizeHeader()
    {
        var session = _contextAccessor.HttpContext.Session.GetString("Token");
        var token = JsonConvert.DeserializeObject<string>(session);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }


}
