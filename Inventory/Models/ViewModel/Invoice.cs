namespace Inventory.Models.ViewModel
{
    public class Invoice
    {

        public string OrderNumber {  get; set; }
        public decimal Amount { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public decimal SubTotal { get; set; }
    }
}
