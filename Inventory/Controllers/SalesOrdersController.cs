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
                salesOrder.DeliveryDate = DateTime.Now;
                salesOrder.OrderDate = DateTime.Now;
                return View(salesOrder);
            }
            ViewData["product"] = _context.Product.ToList();
            salesOrder = _context.SalesOrder.Find(id.GetValueOrDefault());
            salesOrder.SalesOrderLines = _context.SalesOrderLine.Where(x => x.SalesOrderId == id.GetValueOrDefault()).ToList();
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
                }
                else
                {
                    _context.SalesOrder.Update(salesOrder);

                    foreach (var updatedLine in salesOrder.SalesOrderLines)
                    {
                        var existingLine = _context.SalesOrderLine
                            .SingleOrDefault(line => line.SalesOrderLineId == updatedLine.SalesOrderLineId);

                        if (existingLine != null)
                        {
                            // Update properties of existing SalesOrderLine
                            _context.Entry(existingLine).CurrentValues.SetValues(updatedLine);
                        }
                        else
                        {
                            updatedLine.SalesOrderId = salesOrder.SalesOrderId;
                            // Add new SalesOrderLine if it doesn't exist
                            _context.SalesOrderLine.Add(updatedLine);
                        }
                    }

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
        public IActionResult Delete(int id)
        {
            var objFromDb = _context.SalesOrder.Find(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }

            _context.SalesOrder.Remove(objFromDb);
            _context.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful." });
        }

        public IActionResult Invoice(int? id)
        {
             return View(GetOrderData(id));
        }


        public IActionResult Print(int? id)
        {
            return View(GetOrderData(id));
        }


        public List<InvoicePrint> GetOrderData(int? id)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection"); // Update with your connection string name
            List<InvoicePrint> invoicePrints = new List<InvoicePrint>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $@"
                SELECT m.SalesOrderId Id,
                  m.SalesOrderName, 
                  b.BranchName, 
                  c.CustomerName,
                  FORMAT(m.OrderDate,'MM/dd/yyyy') DeliveryDate,
                  FORMAT(m.DeliveryDate,'MM/dd/yyyy') DeliveryDate,
                  r.CurrencyName, 
                  c.Address, 
                  c.Phone, 
                  c.Email, 
                  c.ContactPerson, 
                  st.SalesTypeName, 
                  m.Remarks, 
                  m.Amount, 
                  m.SubTotal, 
                  m.Discount, 
                  m.Tax, 
                  m.Freight, 
                  m.Total ,
                  p.ProductName, 
                  p.Description, 
                  d.Quantity, 
                  d.Price, 
                  d.Amount Amount_Details, 
                  d.DiscountPercentage, 
                  d.DiscountAmount, 
                  d.SubTotal SubTotal_details, 
                  d.TaxPercentage, 
                  d.TaxAmount, 
                  d.Total Total_Details  
                from 
                  SalesOrder m 
                  left outer join SalesOrderLine d on m.SalesOrderId = d.SalesOrderId 
                  left outer join Customer c on c.CustomerId = m.CustomerId 
                  left outer join Product p on p.Id = d.ProductId 
                  left outer join Branch b on b.Id = m.BranchId 
                  left outer join Currency r on r.CurrencyId = m.CurrencyId 
                  left outer join SalesType st on st.SalesTypeId = m.SalesTypeId  
                WHERE 
                    m.SalesOrderId = {id}";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            InvoicePrint invoice = new InvoicePrint
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                SalesOrderName = reader["SalesOrderName"].ToString(),
                                BranchName = reader["BranchName"].ToString(),
                                CustomerName = reader["CustomerName"].ToString(),
                                DeliveryDate = reader["DeliveryDate"].ToString(),
                                CurrencyName = reader["CurrencyName"].ToString(),
                                Address = reader["Address"].ToString(),
                                Phone = reader["Phone"].ToString(),
                                Email = reader["Email"].ToString(),
                                ContactPerson = reader["ContactPerson"].ToString(),
                                SalesTypeName = reader["SalesTypeName"].ToString(),
                                Remarks = reader["Remarks"].ToString(),
                                Amount = Convert.ToDecimal(reader["Amount"]),
                                SubTotal = Convert.ToDecimal(reader["SubTotal"]),
                                Discount = Convert.ToDecimal(reader["Discount"]),
                                Tax = Convert.ToDecimal(reader["Tax"]),
                                Freight = Convert.ToDecimal(reader["Freight"]),
                                Total = Convert.ToDecimal(reader["Total"]),
                                ProductName = reader["ProductName"].ToString(),
                                Description = reader["Description"].ToString(),
                                Quantity = Convert.ToInt32(reader["Quantity"]),
                                Price = Convert.ToDecimal(reader["Price"]),
                                Amount_Details = Convert.ToDecimal(reader["Amount_Details"]),
                                DiscountPercentage = Convert.ToDecimal(reader["DiscountPercentage"]),
                                DiscountAmount = Convert.ToDecimal(reader["DiscountAmount"]),
                                SubTotal_details = Convert.ToDecimal(reader["SubTotal_details"]),
                                TaxPercentage = Convert.ToDecimal(reader["TaxPercentage"]),
                                TaxAmount = Convert.ToDecimal(reader["TaxAmount"]),
                                Total_Details = Convert.ToDecimal(reader["Total_Details"])
                            };

                            invoicePrints.Add(invoice);
                        }
                    }
                }
            }
            return invoicePrints;

        }


        #endregion
    }
}

