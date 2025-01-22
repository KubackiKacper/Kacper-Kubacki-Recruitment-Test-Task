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

    [HttpGet]
    public IActionResult AddContracts()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddContracts(AddContractRequest request)
    {
        if (
            string.IsNullOrEmpty(request.ProductionFacilityCode)
            || string.IsNullOrEmpty(request.ProcessEquipmentTypeCode)
            || request.EquipmentQuantity <= 0
        )
        {
            ModelState.AddModelError(string.Empty, "All fields are required and must be valid.");
            return View(request);
        }

        var facility = await _db
            .ProductionFacility.Include(f => f.Contracts)
            .ThenInclude(c => c.ProcessEquipmentType)
            .FirstOrDefaultAsync(f => f.Code == request.ProductionFacilityCode);

        if (facility == null)
        {
            ModelState.AddModelError(string.Empty, "Production facility not found.");
            return View(request);
        }

        var equipmentType = await _db.ProcessEquipmentType.FirstOrDefaultAsync(e =>
            e.Code == request.ProcessEquipmentTypeCode
        );

        if (equipmentType == null)
        {
            ModelState.AddModelError(string.Empty, "Process equipment type not found.");
            return View(request);
        }

        int requiredArea = request.EquipmentQuantity * equipmentType.Area;

        int occupiedArea = facility.Contracts.Sum(c =>
            c.EquipmentQuantity * c.ProcessEquipmentType.Area
        );

        int availableArea = facility.StandardArea - occupiedArea;

        if (requiredArea > availableArea)
        {
            ModelState.AddModelError(
                string.Empty,
                $"Not enough available area in the production facility. "
                    + $"Required: {requiredArea}, Available: {availableArea}"
            );
            return View(request);
        }

        var newContract = new EquipmentPlacementContract
        {
            ProductionFacilityId = facility.Id,
            ProcessEquipmentTypeId = equipmentType.Id,
            EquipmentQuantity = request.EquipmentQuantity,
        };

        _db.EquipmentPlacementContract.Add(newContract);
        await _db.SaveChangesAsync();

        return RedirectToAction("GetContracts");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(
            new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }
        );
    }
}
