using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    public class ProductType
    {
        public int Id { get; set; }
        [Display(Name = "Product Type Name")]
        public string TypeName { get; set; }
        public string Description { get; set; }
    }
}
