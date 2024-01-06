namespace Inventory.Models.ViewModel
{
    public class Stock
    {
        public string ProductName { get; set; }
        public string BranchName { get; set; }
        public int Id { get; set; }

        public int TotalPurchase { get; set; }
        public int TotalSales { get; set;  }
        public int CurrentStock { get; set; }

    }
}
