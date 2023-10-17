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
    public class WarehousesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WarehousesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Upsert(int? id)
        {
            Warehouse warehouse = new Warehouse();
            if (id == null)
            {
                return View(warehouse);
            }
            warehouse = _context.Warehouse.Find(id.GetValueOrDefault());
            if (warehouse == null)
            {
                return NotFound();
            }
            return View(warehouse);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                if (warehouse.WarehouseId == 0)
                {
                    _context.Warehouse.Add(warehouse);
                }
                else
                {
                    _context.Warehouse.Update(warehouse);
                }
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(warehouse);
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _context.Warehouse });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _context.Warehouse.Find(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }

            _context.Warehouse.Remove(objFromDb);
            _context.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful." });
        }


        #endregion
    }
}

