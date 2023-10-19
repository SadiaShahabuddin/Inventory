using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    public class ShipmentType
    {
        public int ShipmentTypeId { get; set; }
        [Required]
        [Display(Name = "Shipment Type Name")]
        public string ShipmentTypeName { get; set; }
        public string Description { get; set; }

    }
}
