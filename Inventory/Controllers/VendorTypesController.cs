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
    public class VendorTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VendorTypesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Upsert(int? id)
        {
            VendorType vendorType = new VendorType();
            if (id == null)
            {
                return View(vendorType);
            }
            vendorType = _context.VendorType.Find(id.GetValueOrDefault());
            if (vendorType == null)
            {
                return NotFound();
            }
            return View(vendorType);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(VendorType vendorType)
        {
            if (ModelState.IsValid)
            {
                if (vendorType.VendorTypeId == 0)
                {
                    _context.VendorType.Add(vendorType);
                }
                else
                {
                    _context.VendorType.Update(vendorType);
                }
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vendorType);
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _context.VendorType });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _context.VendorType.Find(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }

            _context.VendorType.Remove(objFromDb);
            _context.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful." });
        }


        #endregion
    }
}
