using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    public class SalesType
    {
        [Key]
        public int SalesTypeId { get; set; }
        [Required]
        [Display(Name = "Sales Type Name")]
        public string SalesTypeName { get; set; }
        public string Description { get; set; }
    }
}
