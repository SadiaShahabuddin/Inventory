﻿using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    public class SalesOrderLine
    {
        [Key]
        public int SalesOrderLineId { get; set; }
        [Display(Name = "Sales Order")]
        public int SalesOrderId { get; set; }= 0;
        [Display(Name = "Sales Order")]
        public virtual SalesOrder? SalesOrder { get; set; }
        [Display(Name = "Product Item")]
        public int ProductId { get; set; }
        public string? Description { get; set; } = "";
        public double Quantity { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
        [Display(Name = "Disc %")]
        public double DiscountPercentage { get; set; }
        public double DiscountAmount { get; set; }
        public double SubTotal { get; set; }
        [Display(Name = "Tax %")]
        public double TaxPercentage { get; set; }
        public double TaxAmount { get; set; }

        public double Total { get; set; }

    }
}
