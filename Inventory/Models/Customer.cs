using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Inventory.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Display(Name = "Customer Type")]
        
        public int CustomerTypeId { get; set; }
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
        [ForeignKey("CustomerTypeId")]
        public virtual CustomerType? CustomerType { get; set; }
    }
}
