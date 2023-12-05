namespace Inventory.Models.ViewModel
{
    public class PurchaseInvoice
    {
        public string PurchaseOrderName { get; set; }
        public string BranchName { get; set; }
        public string VendorName { get; set; }
        public string DeliveryDate { get; set; }
        public string CurrencyName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ContactPerson { get; set; }
        public string PurchaseTypeName { get; set; }
        public string Remarks { get; set; }
        public decimal Amount { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public decimal Freight { get; set; }
        public decimal Total { get; set; }

        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Amount_Details { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal SubTotal_details { get; set; }
        public decimal TaxPercentage { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal Total_Details { get; set; }
    }
}
