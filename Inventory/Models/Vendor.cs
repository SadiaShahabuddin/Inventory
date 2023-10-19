using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    public class Vendor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Vendor Name")]
        public string VendorName { get; set; }
        [Display(Name = "Vendor Type")]
        public int VendorTypeId { get; set; }
        [Display(Name = "Street Address")]
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }
    }
}
