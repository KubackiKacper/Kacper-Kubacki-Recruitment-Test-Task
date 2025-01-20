using System.Diagnostics;
using kacper_kubacki.Data;
using kacper_kubacki.ErrorModel;
using kacper_kubacki.Models;
using Microsoft.AspNetCore.Mvc;

namespace kacper_kubacki.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly SqlDbContext _db;

    public HomeController(ILogger<HomeController> logger, SqlDbContext db)
    {
        _logger = logger;
        _db = db;
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
        return View(
            new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }
        );
    }
}
