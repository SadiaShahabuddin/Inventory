using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Models
{
    public class SalesOrder
    {
        [Key]
        public int SalesOrderId { get; set; }
        [Display(Name = "Order Number")]
        public string SalesOrderName { get; set; }
        [Display(Name = "Branch")]
        public int BranchId { get; set; }
        [Required]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }
       
        public DateTimeOffset OrderDate { get; set; } = DateTime.Now.Date;
        public DateTimeOffset DeliveryDate { get; set; } = DateTime.Now.Date;

        [Display(Name = "Currency")]
        public int CurrencyId { get; set; }


        [Display(Name = "Customer Ref. Number")]
        public string? CustomerRefNumber { get; set; }
        [Display(Name = "Sales Type")]
        public int SalesTypeId { get; set; }
        public string Remarks { get; set; }
        public double Amount { get; set; }
        public double SubTotal { get; set; }
        public double Discount { get; set; }
        public double Tax { get; set; }
        public double Freight { get; set; }
        public double Total { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer? Customer { get; set; } 
        [ForeignKey("CurrencyId")]
        public virtual Currency? Currency { get; set; }
        [ForeignKey("SalesTypeId")]
        public virtual SalesType? SalesType { get; set; }
        [ForeignKey("BranchId")]
        public virtual Branch? Branch { get; set; }

        public List<SalesOrderLine>? SalesOrderLines { get; set; }
        public string? SalesInvoiceName { get; set; }

    }
}
