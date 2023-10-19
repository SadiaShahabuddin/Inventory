using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    public class PaymentType
    {
        public int PaymentTypeId { get; set; }
        [Required]
        [Display(Name = "Payment Type Name")]
        public string PaymentTypeName { get; set; }
        public string Description { get; set; }
    }
}

