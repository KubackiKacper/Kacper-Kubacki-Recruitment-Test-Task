using System.Diagnostics;
using kacper_kubacki.Data;
using kacper_kubacki.ErrorModel;
using kacper_kubacki.Models;
using kacper_kubacki.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kacper_kubacki.Controllers;

public class ContractsController : Controller
{
    private readonly ILogger<ContractsController> _logger;
    private readonly SqlDbContext _db;

    public ContractsController(ILogger<ContractsController> logger, SqlDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetContracts()
    {
        var contracts = await _db
            .EquipmentPlacementContract.Include(c => c.ProductionFacility)
            .Include(c => c.ProcessEquipmentType)
            .Select(c => new ContractViewModel
            {
                ProductionFacilityName = c.ProductionFacility.Name,
                ProcessEquipmentTypeName = c.ProcessEquipmentType.Name,
                EquipmentQuantity = c.EquipmentQuantity,
            })
            .ToListAsync();

        return View(contracts);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(
            new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }
        );
    }
}
