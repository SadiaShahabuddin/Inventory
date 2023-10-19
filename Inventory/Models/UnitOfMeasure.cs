using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    public class UnitOfMeasure
    {
        public int UnitOfMeasureId { get; set; }
        [Required]
        [Display(Name = "Unit of Measure Name")]
        public string UnitOfMeasureName { get; set; }
        public string Description { get; set; }
    }
}
