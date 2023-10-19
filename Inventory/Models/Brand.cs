using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    public class Brand
    {
        public int Id { get; set; }
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }
    }
}
