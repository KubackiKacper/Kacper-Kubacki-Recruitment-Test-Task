using kacper_kubacki.Models;
using Microsoft.EntityFrameworkCore;

namespace kacper_kubacki.ViewModels
{
    public class ContractViewModel
    {
        public string ProductionFacilityName { get; set; }
        public string ProcessEquipmentTypeName { get; set; }
        public int EquipmentQuantity { get; set; }
    }
}
