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
    public class PurchaseOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchaseOrdersController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Upsert(int? id)
        {
            PurchaseOrder purchaseOrder = new PurchaseOrder();
            if (id == null)
            {
                return View(purchaseOrder);
            }
            purchaseOrder = _context.PurchaseOrder.Find(id.GetValueOrDefault());
            if (purchaseOrder == null)
            {
                return NotFound();
            }
            return View(purchaseOrder);

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
                    _context.PurchaseOrderLine.AddRange(purchaseOrder.PurchaseOrderLines);
                }
                else
                {
                    _context.PurchaseOrder.Update(purchaseOrder);
                    _context.PurchaseOrderLine.UpdateRange(purchaseOrder.PurchaseOrderLines);
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
            return Json(new { data = _context.PurchaseOrder.Include(p => p.Vendor).Include(p => p.Currency).Include(p => p.PurchaseType).Include(p => p.Branch) });
      
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

        public IActionResult Print(int? id)
        {
            return View();
        }
        #endregion
    }
}
