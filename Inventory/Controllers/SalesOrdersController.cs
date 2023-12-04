using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Inventory.Data;
using Inventory.Models;



namespace Inventory.Controllers
{
    public class SalesOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesOrdersController(ApplicationDbContext context)
        {
            _context = context;
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
            return View();
        }
        public IActionResult Print(int? id)
        {
            return View();
        }
        #endregion
    }
}

