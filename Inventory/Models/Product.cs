using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        public string Description { get; set; }
        public Byte[] Image { get; set; }
        public int Price { get; set; }

        [Required]
        [Display(Name = "Sub Category")]
        public int SubCategoryId { get; set; }

        [Required]
        [Display(Name = "Brand")]
        public int BrandId { get; set; }
        [ForeignKey("SubCategoryId")]
        [Display(Name = "Sub Category")]
        public virtual SubCategory SubCategory { get; set; }


        [ForeignKey("BrandId")]
        public virtual Brand Brand { get; set; }

        [NotMapped]
        public string CategoryName { get; set; }

    }
}