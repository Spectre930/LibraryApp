using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace LibraryApp.Web.Areas.Admin.Controllers;

public class ClientsController : Controller
{
    private readonly HttpClient _client;
    Uri ba = new Uri("https://localhost:44395/api/");
    public ClientsController(HttpClient client)
    {
        _client = client;
        _client.BaseAddress = ba;
    }
    public async Task<IActionResult> Index()
    {
        var response = await _client.GetAsync("Clients/getall");
        response.EnsureSuccessStatusCode();
        var responseStream = response.Content.ReadAsStringAsync().Result;

        var ResObject = JsonConvert.DeserializeObject<IEnumerable<Clients>>(responseStream);


        return View(ResObject);
    }

    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ClientsDto client)
    {
        client.RolesId = 2;
        var obj = JsonConvert.SerializeObject(client);
        await _client.PostAsJsonAsync("Clients/create", client);
        TempData["success"] = "Client Created Successfully";
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        var response = await _client.GetAsync($"Clients/get/id?id={id}");
        response.EnsureSuccessStatusCode();
        var responseStream = response.Content.ReadAsStringAsync().Result;

        var ResObject = JsonConvert.DeserializeObject<Clients>(responseStream);
        return View(ResObject);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Clients client)
    {
        var obj = JsonConvert.SerializeObject(client);
        await _client.PutAsJsonAsync($"Clients/id?id={client.Id}/update", client);
        TempData["success"] = "Client Updated Successfully";
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int? id)
    {
        var response = await _client.GetAsync($"Clients/get/id?id={id}");
        response.EnsureSuccessStatusCode();
        var responseStream = response.Content.ReadAsStringAsync().Result;
        var ResObject = JsonConvert.DeserializeObject<Clients>(responseStream);

        return View(ResObject);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePOST(int? id)
    {
        var req = new HttpRequestMessage(HttpMethod.Delete, $"Clients/delete/{id}");
        await _client.SendAsync(req);
        TempData["success"] = "Client Deleted Successfully";
        return RedirectToAction("Index");
    }

}
