using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    public class PurchaseType

    {

        [Key]
        public int PurchaseTypeId { get; set; }
        [Required]

        [Display(Name = "Purchase Type Name")]
        public string PurchaseTypeName { get; set; }
        public string Description { get; set; }
    }
}
