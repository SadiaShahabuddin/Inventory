using Inventory.Data;
using Inventory.Models;
using Inventory.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static Inventory.MainMenu.MainMenu;

namespace Inventory.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public HomeController(ApplicationDbContext context, IConfiguration configuration, ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var last10Orders = _context.SalesOrder
                .OrderByDescending(order => order.OrderDate)
                .Take(10)
                .ToList();
            var last10OProduct = _context.Product
                .OrderByDescending(order => order.Id)
                .Take(10)
                .ToList();
            HomeDash homeDash = new HomeDash()
            {
                Products= last10OProduct,
                SalesOrders = last10Orders,
                TotalProduct= _context.Product.Count(),
                TotalCustomer = _context.Customer.Count(),
                TotalPurchase= _context.PurchaseOrder.Sum(order => order.Total),
                TotalSales = _context.SalesOrder.Sum(order => order.Total)

        };
            return View(homeDash);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}