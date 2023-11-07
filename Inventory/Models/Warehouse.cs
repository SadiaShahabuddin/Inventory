using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Models
{
    public class Warehouse
    {
        [Key]
        public int WarehouseId { get; set; }
        [Required]
        [Display(Name = "Warehouse Name")]
        public string WarehouseName { get; set; }
        public string Description { get; set; }
        [Display(Name = "Branch")]
        public int BranchId { get; set; }
        [ForeignKey("BranchId")]
        public virtual Branch? Branch { get; set; }
    }
}
