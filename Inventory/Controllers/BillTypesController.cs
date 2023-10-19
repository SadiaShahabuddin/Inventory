using Microsoft.EntityFrameworkCore;
using Inventory.Data;
using Inventory.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
    public class BillTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BillTypesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Upsert(int? id)
        {
            BillType billType = new BillType();
            if (id == null)
            {
                return View(billType);
            }
            billType = _context.BillType.Find(id.GetValueOrDefault());
            if (billType == null)
            {
                return NotFound();
            }
            return View(billType);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(BillType billType)
        {
            if (ModelState.IsValid)
            {
                if (billType.BillTypeId == 0)
                {
                    _context.BillType.Add(billType);
                }
                else
                {
                    _context.BillType.Update(billType);
                }
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(billType);
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _context.BillType });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _context.BillType.Find(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }

            _context.BillType.Remove(objFromDb);
            _context.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful." });
        }


        #endregion
    }
}
