using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Inventory.MainMenu.MainMenu;

namespace Inventory.Models
{
    public class PurchaseOrder
    {
        [Key]
        public int PurchaseOrderId { get; set; }
        [Display(Name = "Order Number")]
        public string PurchaseOrderName { get; set; }
        [Display(Name = "Branch")]
        public int BranchId { get; set; }
        [ForeignKey("BranchId")]
        public virtual Branch? Branch { get; set; }
        [Display(Name = "Vendor")]
        public int VendorId { get; set; }
        [ForeignKey("VendorId")]
        public virtual Vendor? Vendor { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public DateTimeOffset DeliveryDate { get; set; }

        [Display(Name = "Currency")]
        public int CurrencyId { get; set; }
        [ForeignKey("CurrencyId")]
        public virtual Currency? Currency { get; set; }

        [Display(Name = "Purchase Type")]
        public int PurchaseTypeId { get; set; }
        [ForeignKey("PurchaseTypeId")]
        public virtual PurchaseType? PurchaseType { get; set; }
        public string? Remarks { get; set; }
        public double Amount { get; set; }
        public double SubTotal { get; set; }
        public double Discount { get; set; }
        public double Tax { get; set; }
        public double Freight { get; set; }
        public double Total { get; set; }
        public List<PurchaseOrderLine>? PurchaseOrderLines { get; set; }


    }
}
