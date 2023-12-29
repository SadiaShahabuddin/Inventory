using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Inventory.Data;
using Inventory.Models;
using Microsoft.Data.SqlClient;
using Inventory.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Inventory.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public ProductsController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            ViewBag.CategoryId = _context.Category.ToList();
            var applicationDbContext = _context.Product.Include(s => s.SubCategory).Include(s => s.Brand).Include(s => s.Branch).Include(s => s.Currency).Include(s => s.UnitOfMeasure);
            return View(await applicationDbContext.ToListAsync());

        }
       
        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewBag.CategoryId = _context.Category.ToList();
            ViewBag.SubCategoryId = _context.SubCategory.ToList();
            ViewBag.BrandId = _context.Brand.ToList();
            ViewBag.CurrencyId = _context.Currency.ToList();
            ViewBag.BranchId = _context.Branch.ToList();
            ViewBag.UnitOfMeasureId = _context.UnitOfMeasure.ToList();
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            var files = HttpContext.Request.Form.Files;
            if (files.Count > 0)
            {
                byte[] p1 = null;
                using (var fs1 = files[0].OpenReadStream())
                {
                    using (var ms1 = new MemoryStream())
                    {
                        fs1.CopyTo(ms1);
                        p1 = ms1.ToArray();
                    }
                }
                product.Image = p1;
            }
            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            ViewBag.CategoryId = _context.Category.ToList();
            ViewBag.SubCategoryId = _context.SubCategory.ToList();
            ViewBag.BrandId = _context.Brand.ToList();
            ViewBag.CurrencyId = _context.Currency.ToList();
            ViewBag.BranchId = _context.Branch.ToList();
            ViewBag.UnitOfMeasureId = _context.UnitOfMeasure.ToList();
            var product = await _context.Product.FindAsync(id);
            
            if (product == null)
            {
                return NotFound();
            }
            product.CategoryId = _context.SubCategory.FirstOrDefault(x => x.Id == product.SubCategoryId).CategoryId;
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
           

            if (id != product.Id)
            {
                return NotFound();
            }

            try
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    byte[] p1 = null;
                    using (var fs1 = files[0].OpenReadStream())
                    {
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                    }
                    product.Image = p1;
                }
                if(product.Image==null)
                {
                    product.Image = _context.Product.AsNoTracking().FirstOrDefault(e => e.Id == id).Image;

                }
                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Brand)
                .Include(x => x.SubCategory)
                .Include(p => p.UnitOfMeasure)
                .Include(p => p.Branch)
                .Include(p => p.Currency)
                .Select(p => new
                {
                    Product = p,
                    CategoryName = p.SubCategory.Category.Name
                })
                .FirstOrDefaultAsync(m => m.Product.Id == id);

            Product obj = product.Product;
            obj.CategoryName = product.CategoryName;
            return View(obj);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Product == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Product'  is null.");
            }
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return (_context.Product?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        [HttpGet]
        public async Task<IActionResult> GetSubcategories(int? categoryId)
        {
            var subCategory = await _context.SubCategory.Where(x => x.CategoryId == categoryId).ToListAsync();
            var json= Json(subCategory);
            return json;
        }

        [HttpGet]

        public IActionResult GetAll()
        {
            return Json(new { data = _context.Product });
        }

        public IActionResult Stock()
        {
            return View();
        }

        public JsonResult GetStock()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var usersWithBranch = _context.ApplicationUser.FirstOrDefault(x => x.Id == userId);
            var BranchId = usersWithBranch.BranchId;

            List<Stock> stocks = new List<Stock>();
            string connectionString = _configuration.GetConnectionString("DefaultConnection"); // Update with your connection string name
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "";
                if (usersWithBranch.BranchId ==null)
                {
                    query = @"
                   SELECT P.ProductName,X.BranchName, ISNULL(X.TOTALPURCHASE, 0) AS TOTALPURCHASE, ISNULL(Y.TOTALSALES, 0) AS TOTALSALES, (ISNULL(X.TOTALPURCHASE, 0) - ISNULL(Y.TOTALSALES, 0)) AS CURRENTSTOCK
                   FROM PRODUCT P
                   LEFT OUTER JOIN (
                       SELECT SUM(QUANTITY) AS TOTALPURCHASE, PRODUCTID, MIN(B.BranchName)BranchName
                       FROM PURCHASEORDERLINE L, PurchaseOrder M, Branch B
					   WHERE L.PurchaseOrderId= M.PurchaseOrderId AND B.Id= M.BranchId
                       GROUP BY PRODUCTID
                   ) X ON X.PRODUCTID = P.ID
                   LEFT OUTER JOIN (
                       SELECT SUM(QUANTITY) AS TOTALSALES, PRODUCTID, MIN(B.BranchName)BranchName
                       FROM SALESORDERLINE L, SalesOrder M, Branch B
					   WHERE L.SalesOrderId= M.SalesOrderId AND B.Id= M.BranchId
                       GROUP BY PRODUCTID
                   ) Y ON Y.PRODUCTID = P.ID
                   ORDER BY P.Id";


                }
                else
                {
                     query = $@"
    SELECT P.ProductName,X.BranchName, ISNULL(X.TOTALPURCHASE, 0) AS TOTALPURCHASE, ISNULL(Y.TOTALSALES, 0) AS TOTALSALES, (ISNULL(X.TOTALPURCHASE, 0) - ISNULL(Y.TOTALSALES, 0)) AS CURRENTSTOCK
    FROM PRODUCT P
    LEFT OUTER JOIN (
        SELECT SUM(QUANTITY) AS TOTALPURCHASE, PRODUCTID, MIN(B.BranchName)BranchName
        FROM PURCHASEORDERLINE L, PurchaseOrder M, Branch B
        WHERE L.PurchaseOrderId = M.PurchaseOrderId AND B.Id = M.BranchId
        AND M.BranchId = {BranchId}
        GROUP BY PRODUCTID
    ) X ON X.PRODUCTID = P.ID
    LEFT OUTER JOIN (
        SELECT SUM(QUANTITY) AS TOTALSALES, PRODUCTID, MIN(B.BranchName)BranchName
        FROM SALESORDERLINE L, SalesOrder M, Branch B
        WHERE L.SalesOrderId = M.SalesOrderId AND B.Id = M.BranchId
        AND M.BranchId = {BranchId}
        GROUP BY PRODUCTID
    ) Y ON Y.PRODUCTID = P.ID
    ORDER BY P.Id";
                }
                

                using (SqlCommand command = new SqlCommand(query, connection))
               {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Stock stock = new Stock
                            {
                                BranchName = reader["BranchName"].ToString(),
                                ProductName = reader["ProductName"].ToString(),
                                TotalPurchase = Convert.ToInt32(reader["TOTALPURCHASE"]),
                                TotalSales = Convert.ToInt32(reader["TOTALSALES"]),
                                CurrentStock = Convert.ToInt32(reader["CURRENTSTOCK"])
                            };

                            stocks.Add(stock);
                        }
                    }
                }
            }
            return Json(new { data = stocks });

        }
    }
}

