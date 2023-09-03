using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;
using LibraryApp.Models.ViewModels;
using LibraryApp.Web.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;


namespace LibraryApp.Web.Repository;

public class BooksHttp : RepositoryHttp<Books>, IBooksHttp
{
    private readonly HttpClient _client;
    private readonly Uri ba = new("https://localhost:44395/api/");
    public BooksHttp(HttpClient client, IHttpContextAccessor contextAccessor) : base(client, contextAccessor)
    {
        _client = client;
        _client.BaseAddress = ba;
    }

    public async Task<IEnumerable<BooksIndexVM>> GetAllBooks()
    {
        AuthorizeHeader();
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
        AuthorizeHeader();
        var response = await _client.GetAsync($"Books/getauthors/{id}");
        response.EnsureSuccessStatusCode();
        var responseStream = response.Content.ReadAsStringAsync().Result;
        var authors = JsonConvert.DeserializeObject<IEnumerable<String>>(responseStream);
        return authors;
    }

    public async Task<BooksIndexVM> GetBook(int id)
    {
        AuthorizeHeader();
        var response = await _client.GetAsync($"Books/{id}");
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
        AuthorizeHeader();
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
        var response = await _client.GetAsync($"Books/{id}");
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
        AuthorizeHeader();
        var obj = JsonConvert.SerializeObject(editVM.book);
        var request = new HttpRequestMessage(HttpMethod.Put, _client.BaseAddress + "Books/update");
        request.Headers.Add("accept", "text/plain");
        var content = new StringContent(obj, null, "application/json");
        request.Content = content;
        var response = await _client.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }

    public async Task BorrowBook(int boookId)
    {
        AuthorizeHeader();
        var user = GetClaims();
        var borrow = new BorrowDto()
        {
            BookId = boookId,
            ClientId = user.Id,
            BorrowDate = DateTime.Now,
            ReturnDate = DateTime.Now.AddDays(14)
        };

        var response = await _client.PostAsJsonAsync("Borrow/create", borrow);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response.Content.ReadAsStringAsync().Result);
        }

    }

    public async Task ReturnBook(int borrowId)
    {
        AuthorizeHeader();
        var response = await _client.PostAsJsonAsync($"Borrow/return/{borrowId}", borrowId);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response.Content.ReadAsStringAsync().Result);
        }
    }

    public async Task PurchaseBook(PurchaseVM purchaseVM)
    {
        AuthorizeHeader();
        var dto = new PurchasesDto()
        {
            ClientId = GetClaims().Id,
            BookId = purchaseVM.book.book.Id,
            Quantity = purchaseVM.quantity
        };

        var response = await _client.PostAsJsonAsync("Purchases/create", dto);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response.Content.ReadAsStringAsync().Result);
        }

    }

    public async Task<IEnumerable<BooksIndexVM>> GetBooksOfAuthor(int authorId)
    {
        AuthorizeHeader();
        var response = await _client.GetAsync($"Authors/{authorId}/books");
        if (response.IsSuccessStatusCode)
        {
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
        throw new Exception("Authors has no books");

    }
}

