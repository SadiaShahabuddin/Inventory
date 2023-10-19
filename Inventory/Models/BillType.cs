using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    public class BillType

    {
        public int BillTypeId { get; set; }
        [Required]
        [Display(Name = "Bill Type Name")]
        public string BillTypeName { get; set; }
        public string Description { get; set; }
    }
}
