namespace Inventory.Models.ViewModel
{
    public class HomeDash
    {
        public List<Product> Products { get; set; }
        public List<SalesOrder> SalesOrders { get; set; }
        public double TotalSales { get; set; }
        public double TotalPurchase { get; set; }
        public int TotalCustomer { get; set; }
        public int TotalProduct { get; set; }


    }
}
