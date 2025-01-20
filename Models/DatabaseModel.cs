using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kacper_kubacki.Models
{
    public class ProductionFacility
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int StandardArea { get; set; }

        public ICollection<EquipmentPlacementContract> Contracts { get; set; }
    }

    public class ProcessEquipmentType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Area { get; set; }
    }

    public class EquipmentPlacementContract
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ProductionFacility")]
        public int ProductionFacilityId { get; set; }
        public ProductionFacility ProductionFacility { get; set; }

        [ForeignKey("ProcessEquipmentType")]
        public int ProcessEquipmentTypeId { get; set; }
        public ProcessEquipmentType ProcessEquipmentType { get; set; }

        [Required]
        public int EquipmentQuantity { get; set; }
    }
}
