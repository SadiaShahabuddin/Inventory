using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    public class CashBank
    {
        [Key]
        public int CashBankId { get; set; }
        [Display(Name = "Cash / Bank Name")]
        public string CashBankName { get; set; }
        public string Description { get; set; }
    }
}
