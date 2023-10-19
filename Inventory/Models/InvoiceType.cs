using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    public class InvoiceType
    {
        public int InvoiceTypeId { get; set; }
        [Required]
        [Display(Name = "Invoice Type Name")]
        public string InvoiceTypeName { get; set; }
        public string Description { get; set; }
    }
}
