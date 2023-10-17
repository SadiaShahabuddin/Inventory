using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    public class Warehouse
    {
        [Key]
        public int WarehouseId { get; set; }
        [Required]
        public string WarehouseName { get; set; }
        public string Description { get; set; }
        [Display(Name = "Branch")]
        public int BranchId { get; set; }
    }
}
