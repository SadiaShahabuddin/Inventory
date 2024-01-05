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
using System.Net;
using System.Security.Claims;

namespace Inventory.Controllers
{
    public class PurchaseOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public PurchaseOrdersController(ApplicationDbContext context, IConfiguration configuration)
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
            Models.PurchaseOrder purchaseOrder = new PurchaseOrder();
            ViewData["product"] = _context.Product.ToList();
            ViewData["branchId"] = _context.ApplicationUser
            .Where(user => user.Id == User.FindFirstValue(ClaimTypes.NameIdentifier))
            .Select(user => user.BranchId)
            .FirstOrDefault();
            if (id == null)
            {
                purchaseOrder.DeliveryDate = DateTime.Now;
                purchaseOrder.OrderDate = DateTime.Now;
                purchaseOrder.PurchaseOrderName  = GenerateOrderNumber();
                return View(purchaseOrder);
            }
            purchaseOrder = _context.PurchaseOrder.Find(id.GetValueOrDefault());
            purchaseOrder.PurchaseOrderLines = _context.PurchaseOrderLine.Where(x => x.PurchaseOrderId == id.GetValueOrDefault()).ToList();
            if (purchaseOrder == null)
            {
                return NotFound();
            }
            return View(purchaseOrder);

        }
        private string GenerateOrderNumber()
        {
            // Use current date and time information
            DateTime now = DateTime.Now;

            // Format the date and time information to include in the order number
            string datePart = now.ToString("yyyy-MM-dd");
            string timePart = now.ToString("HHmmssfff");

            // Combine the formatted date and time information with a prefix
            string orderNumber = $"#PUR-{datePart}{timePart}";

            return orderNumber;
        }
        private string GenerateInvoiceNumber()
        {
            // Use current date and time information
            DateTime now = DateTime.Now;

            // Format the date and time information to include in the order number
            string datePart = now.ToString("yyyy-MM-dd");
            string timePart = now.ToString("HHmmssfff");

            // Combine the formatted date and time information with a prefix
            string orderNumber = $"#INVOICE-{datePart}{timePart}";

            return orderNumber;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(PurchaseOrder purchaseOrder)
        {
            if (ModelState.IsValid)
            {
                if (purchaseOrder.PurchaseOrderId == 0)
                {
                    _context.PurchaseOrder.Add(purchaseOrder);
                }
                else
                {
                    // Update PurchaseOrder
                    _context.PurchaseOrder.Update(purchaseOrder);
                    var oldObj = _context.PurchaseOrderLine.Where(x => x.PurchaseOrderId == purchaseOrder.PurchaseOrderId).ToList();
                    // Update PurchaseOrderLines
                    foreach (var updatedLine in purchaseOrder.PurchaseOrderLines)
                    {
                        var existingLine = _context.PurchaseOrderLine
                            .SingleOrDefault(line => line.PurchaseOrderLineId == updatedLine.PurchaseOrderLineId);

                        if (existingLine != null)
                        {
                            // Update properties of existing PurchaseOrderLine
                            _context.Entry(existingLine).CurrentValues.SetValues(updatedLine);
                        }
                        else
                        {
                            updatedLine.PurchaseOrderId = purchaseOrder.PurchaseOrderId;
                            // Add new PurchaseOrderLine if it doesn't exist
                            _context.PurchaseOrderLine.Add(updatedLine);
                        }
                    }
                    var result = oldObj.Where(p => !purchaseOrder.PurchaseOrderLines.Any(p2 => p2.PurchaseOrderLineId == p.PurchaseOrderLineId));

                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(purchaseOrder);
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {

            var branchId = _context.ApplicationUser
           .Where(user => user.Id == User.FindFirstValue(ClaimTypes.NameIdentifier))
           .Select(user => user.BranchId)
           .FirstOrDefault();
            if (branchId == null)
            {
                return Json(new { data = _context.PurchaseOrder.Include(p => p.Vendor).Include(p => p.Currency).Include(p => p.PurchaseType).Include(p => p.Branch) });

            }
            else
            {
                return Json(new { data = _context.PurchaseOrder.Where(x => x.BranchId == branchId).Include(p => p.Vendor).Include(p => p.Currency).Include(p => p.PurchaseType).Include(p => p.Branch) });

            }

        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _context.PurchaseOrder.Find(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }

            _context.PurchaseOrder.Remove(objFromDb);
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
        public List<PurchaseInvoice> GetOrderData(int? id)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection"); // Update with your connection string name
            List<PurchaseInvoice> purchaseInvoices = new List<PurchaseInvoice>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $@"
                        SELECT
                    m.PurchaseOrderId Id,
                    m.PurchaseOrderName, 
                    b.BranchName, 
                    c.VendorName,
                    FORMAT(m.OrderDate, 'MM/dd/yyyy') DeliveryDate,
                    FORMAT(m.DeliveryDate, 'MM/dd/yyyy') DeliveryDate,
                    r.CurrencyName, 
                    c.Address, 
                    c.Phone, 
                    c.Email, 
                    c.ContactPerson, 
                    st.PurchaseTypeName, 
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
                    PurchaseOrder m
                    left outer join PurchaseOrderLine d on m.PurchaseOrderId = d.PurchaseOrderId
                    left outer join Vendor c on c.Id = m.VendorId
                    left outer join Product p on p.Id = d.ProductId
                    left outer join Branch b on b.Id = m.BranchId
                    left outer join Currency r on r.CurrencyId = m.CurrencyId
                    left outer join PurchaseType st on st.PurchaseTypeId = m.PurchaseTypeId
                   WHERE 
                    m.PurchaseOrderId = {id}";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PurchaseInvoice invoice = new PurchaseInvoice
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                PurchaseOrderName = reader["PurchaseOrderName"].ToString(),
                                BranchName = reader["BranchName"].ToString(),
                                VendorName = reader["VendorName"].ToString(),
                                DeliveryDate = reader["DeliveryDate"].ToString(),
                                CurrencyName = reader["CurrencyName"].ToString(),
                                Address = reader["Address"].ToString(),
                                Phone = reader["Phone"].ToString(),
                                Email = reader["Email"].ToString(),
                                ContactPerson = reader["ContactPerson"].ToString(),
                                PurchaseTypeName = reader["PurchaseTypeName"].ToString(),
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

                            purchaseInvoices.Add(invoice);
                        }
                    }
                }
            }
            return purchaseInvoices;

        }

        #endregion
    }
}
