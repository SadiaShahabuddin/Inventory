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
        public IActionResult Upsert(SalesOrder salesOrder)
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
                _context.SaveChangesAsync();
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


        #endregion
    }
}

