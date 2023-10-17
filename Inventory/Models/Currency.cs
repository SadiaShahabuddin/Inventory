using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    public class Currency
    {
        [Key]
        public int CurrencyId { get; set; }
        [Required]
        public string CurrencyName { get; set; }
        [Required]
        public string CurrencyCode { get; set; }
        public string Description { get; set; }
    }
}
