using LibraryApp.Models.Models;
using LibraryApp.Models.ViewModels;
using LibraryApp.Web.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace LibraryApp.Web.Repository;

public class BooksHttp : RepositoryHttp<Books>, IBooksHttp
{
    private readonly HttpClient _client;
    private Uri ba = new Uri("https://localhost:44395/api/");
    public BooksHttp(HttpClient client) : base(client)
    {
        _client = client;
        _client.BaseAddress = ba;
    }

    public async Task<IEnumerable<BooksIndexVM>> GetAllBooks()
    {
        var response = await _client.GetAsync("Books/getall");
        response.EnsureSuccessStatusCode();
        var responseStream = response.Content.ReadAsStringAsync().Result;
        var books = JsonConvert.DeserializeObject<IEnumerable<Books>>(responseStream);

        var res = new List<BooksIndexVM>();

        foreach (var item in books)
        {
            res.Add(new BooksIndexVM
            {
                book = item,
                authors = await GetAuthrosOfBook(item.Id)
            });
        }

        return res;
    }

    public async Task<IEnumerable<string>> GetAuthrosOfBook(int id)
    {
        var response = await _client.GetAsync($"Books/getauthors/id?id={id}");
        response.EnsureSuccessStatusCode();
        var responseStream = response.Content.ReadAsStringAsync().Result;
        var authors = JsonConvert.DeserializeObject<IEnumerable<String>>(responseStream);
        return authors;
    }

    public async Task<BooksIndexVM> GetBook(int id)
    {
        var response = await _client.GetAsync($"Books/id?id={id}");
        response.EnsureSuccessStatusCode();
        var responseStream = response.Content.ReadAsStringAsync().Result;
        var books = JsonConvert.DeserializeObject<Books>(responseStream);

        var ResObject = new BooksIndexVM()
        {
            book = books,
            authors = await GetAuthrosOfBook(books.Id)
        };



        return ResObject;


    }

    public async Task PostCreatedBook(BooksVM bookVM, List<int> selectedOptions)
    {
        string auth = string.Join(",", selectedOptions.Select(x => x.ToString()).ToArray());

        bookVM.book.AuthorIds = auth;

        await _client.PostAsJsonAsync("Books/create", bookVM.book);
    }

    public BooksVM ViewCreateBookVM(IEnumerable<Genres> genres, IEnumerable<Authors> authors)
    {
        BooksVM booksVM = new()
        {
            book = new(),
            GenresList = genres.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            }),
            AuthorsList = authors

        };
        return booksVM;
    }

    public async Task<BooksEditVM> GetEditBook(int id, IEnumerable<Genres> genres)
    {
        var response = await _client.GetAsync($"Books/id?id={id}");
        response.EnsureSuccessStatusCode();
        var responseStream = response.Content.ReadAsStringAsync().Result;
        var books = JsonConvert.DeserializeObject<Books>(responseStream);

        var ResObject = new BooksEditVM()
        {
            book = books,
            GenresList = genres.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            })
        };

        return ResObject;
    }

    public async Task UpdateBookAsync(BooksEditVM editVM)
    {
        var obj = JsonConvert.SerializeObject(editVM.book);
        var request = new HttpRequestMessage(HttpMethod.Put, _client.BaseAddress+"Books/update");
        request.Headers.Add("accept", "text/plain");
        var content = new StringContent(obj, null, "application/json");
        request.Content = content;
        var response = await _client.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }
}

