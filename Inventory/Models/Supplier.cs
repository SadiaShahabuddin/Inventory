using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    public class Supplier
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNo { get; set; }
        public string AccountNo { get; set; }

        public string Address { get; set; }
        public decimal CreditAmount { get; set; }

    }
}
