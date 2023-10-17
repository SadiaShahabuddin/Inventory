using Microsoft.EntityFrameworkCore;
using Inventory.Data;
using Inventory.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
    public class VendorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VendorsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Upsert(int? id)
        {
            Vendor vendor = new Vendor();
            if (id == null)
            {
                return View(vendor);
            }
            vendor = _context.Vendor.Find(id.GetValueOrDefault());
            if (vendor == null)
            {
                return NotFound();
            }
            return View(vendor);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Vendor vendor)
        { 
            if (ModelState.IsValid)
            {
                if (vendor.Id == 0)
                {
                    _context.Vendor.Add(vendor);
                }
                else
                {
                    _context.Vendor.Update(vendor);
                }
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vendor);
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _context.Vendor });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _context.Vendor.Find(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }

            _context.Vendor.Remove(objFromDb);
            _context.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful." });
        }


        #endregion
    }
}
