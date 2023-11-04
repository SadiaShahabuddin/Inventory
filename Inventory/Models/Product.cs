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
        public string ProductCode { get; set; } = "";
        public string Barcode { get; set; } = "";
        public string Description { get; set; }
        public Byte[] Image { get; set; }
        public string ProductImageUrl { get; set; } = "";

        [Display(Name = "UOM")]
        public int UnitOfMeasureId { get; set; }
        public double DefaultBuyingPrice { get; set; } = 0.0;

        [Display(Name ="Default Selling Price")]
        public double DefaultSellingPrice { get; set; } = 0.0;

        [Required]
        [Display(Name = "Branch")]
        public int BranchId { get; set; } = 0;

        [Display(Name = "Currency")]
        public int CurrencyId { get; set; }
        
        [Required]
        [Display(Name = "Sub Category")]
        public int SubCategoryId { get; set; }
        
        [Required]
        [Display(Name = "Brand")]
        public int BrandId { get; set; }


        //Navigation Property 

        [ForeignKey("SubCategoryId")]
        public virtual SubCategory SubCategory { get; set; }

        [ForeignKey("BrandId")]
        public virtual Brand Brand { get; set; }

        [ForeignKey("BranchId")]
        public virtual Branch Branch { get; set; }

        [ForeignKey("UnitOfMeasureId")]
        public virtual UnitOfMeasure UnitOfMeasure { get; set; }

        [ForeignKey("CurrencyId")]
        public virtual Currency Currency { get; set; }
        [NotMapped]
        public string CategoryName { get; set; }
    }
}