using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Inventory.Data;
using Inventory.Models;
using Inventory.Models.ViewModel;
using Microsoft.Data.SqlClient;

namespace Inventory.Controllers
{
    public class SalesOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        public SalesOrdersController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Upsert(int? id)
        {
            SalesOrder salesOrder = new SalesOrder();
            if (id == null)
            {
                return View(salesOrder);
            }
            salesOrder = _context.SalesOrder.Find(id.GetValueOrDefault());
            if (salesOrder == null)
            {
                return NotFound();
            }
            return View(salesOrder);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(SalesOrder salesOrder)
        {
            if (ModelState.IsValid)
            {
                if (salesOrder.SalesOrderId == 0)
                {
                    _context.SalesOrder.Add(salesOrder);
                    _context.SalesOrderLine.AddRange(salesOrder.SalesOrderLines);
                }
                else
                {
                    _context.SalesOrder.Update(salesOrder);
                    _context.SalesOrderLine.UpdateRange(salesOrder.SalesOrderLines);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(salesOrder);
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _context.SalesOrder.Include(p => p.Customer).Include(p => p.Currency).Include(p => p.SalesType).Include(p => p.Branch) });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var objFromDb = _context.SalesOrder.Find(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }

            _context.SalesOrder.Remove(objFromDb);
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful." });
        }

        public IActionResult Invoice(int? id)
        {
       
                List<Invoice> invoices = new List<Invoice>();

                string connectionString = _configuration.GetConnectionString("DefaultConnection"); // Update with your connection string name

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlQuery = @"
                SELECT
                    y.SalesOrderName AS OrderNumber,
                    y.Amount,
                    p.ProductName,
                    s.Quantity,
                    y.SubTotal
                FROM
                    SalesOrder y
                LEFT JOIN
                    SalesOrderLine s ON y.SalesOrderId = s.SalesOrderId
                LEFT JOIN
                    Product p ON s.ProductId = p.Id";

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Invoice invoice = new Invoice
                                {
                                    OrderNumber = reader["OrderNumber"].ToString(),
                                    Amount = Convert.ToDecimal(reader["Amount"]),
                                    ProductName = reader["ProductName"].ToString(),
                                    Quantity = Convert.ToInt32(reader["Quantity"]),
                                    SubTotal = Convert.ToDecimal(reader["SubTotal"])
                                };

                                invoices.Add(invoice);
                            }
                        }
                    }
                }

            return View(invoices);
        }
        public IActionResult Print(int? id)
        {
            return View();
        }
        #endregion
    }
}

