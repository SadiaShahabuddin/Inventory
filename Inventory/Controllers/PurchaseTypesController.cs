using Microsoft.EntityFrameworkCore;
using Inventory.Data;
using Inventory.Models;
using Microsoft.AspNetCore.Mvc;


namespace Inventory.Controllers
{
    public class PurchaseTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchaseTypesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Upsert(int? id)
        {
            PurchaseType purchaseType = new PurchaseType();
            if (id == null)
            {
                return View(purchaseType);
            }
            purchaseType = _context.PurchaseType.Find(id.GetValueOrDefault());
            if (purchaseType == null)
            {
                return NotFound();
            }
            return View(purchaseType);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(PurchaseType purchaseType)
        {
            if (ModelState.IsValid)
            {
                if (purchaseType.PurchaseTypeId == 0)
                {
                    _context.PurchaseType.Add(purchaseType);
                }
                else
                {
                    _context.PurchaseType.Update(purchaseType);
                }
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(purchaseType);
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _context.PurchaseType });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _context.PurchaseType.Find(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }

            _context.PurchaseType.Remove(objFromDb);
            _context.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful." });
        }


        #endregion
    }
}
