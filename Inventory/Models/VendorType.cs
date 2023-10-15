using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    public class VendorType
    {
        [Key]
        public int VendorTypeId { get; set; }
        [Required]
        public string VendorTypeName { get; set; }
        public string Description { get; set; }
    }
}
