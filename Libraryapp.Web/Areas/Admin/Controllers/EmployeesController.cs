using LibraryApp.Models.DTO;
using LibraryApp.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LibraryApp.Web.Areas.Admin.Controllers;

public class EmployeesController : Controller
{
    private readonly HttpClient _client;
    Uri ba = new Uri("https://localhost:44395/api/");
    public EmployeesController(HttpClient client)
    {
        _client = client;
        _client.BaseAddress = ba;
    }
    public async Task<IActionResult> Index()
    {
        var response = await _client.GetAsync("Employees/getall");
        response.EnsureSuccessStatusCode();
        var responseStream = response.Content.ReadAsStringAsync().Result;

        var ResObject = JsonConvert.DeserializeObject<IEnumerable<Employees>>(responseStream);


        return View(ResObject);
    }

    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EmployeesDto employee, bool admin)
    {
        if (admin)
        {
            employee.RoleId = 1;
        }
        else
        {
            employee.RoleId = null;
        }
        var obj = JsonConvert.SerializeObject(employee);
        await _client.PostAsJsonAsync("Employees/create", employee);
        TempData["success"] = "Employee Created Successfully";
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        var response = await _client.GetAsync($"Employees/get/id?id={id}");
        response.EnsureSuccessStatusCode();
        var responseStream = response.Content.ReadAsStringAsync().Result;

        var ResObject = JsonConvert.DeserializeObject<Employees>(responseStream);
        return View(ResObject);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Employees client)
    {
        var obj = JsonConvert.SerializeObject(client);
        await _client.PutAsJsonAsync($"Employees/id?id={client.Id}/update", client);
        TempData["success"] = "Client Updated Successfully";
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int? id)
    {
        var response = await _client.GetAsync($"Employees/get/id?id={id}");
        response.EnsureSuccessStatusCode();
        var responseStream = response.Content.ReadAsStringAsync().Result;
        var ResObject = JsonConvert.DeserializeObject<Employees>(responseStream);

        return View(ResObject);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePOST(int? id)
    {
        var req = new HttpRequestMessage(HttpMethod.Delete, $"Employees/delete/{id}");
        await _client.SendAsync(req);
        TempData["success"] = "Client Deleted Successfully";
        return RedirectToAction("Index");
    }
}
