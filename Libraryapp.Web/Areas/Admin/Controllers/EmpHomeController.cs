using LibraryApp.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LibraryApp.Web.Areas.Admin.Controllers;

public class EmpHomeController : Controller
{
    private readonly ILogger<EmpHomeController> _logger;
    private readonly HttpClient _httpClient;
    public EmpHomeController(ILogger<EmpHomeController> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    public IActionResult Index()
    {


        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}