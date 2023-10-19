using Microsoft.EntityFrameworkCore;
using Inventory.Data;
using Inventory.Models;
using Microsoft.AspNetCore.Mvc;


namespace Inventory.Controllers
{
    public class BranchesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BranchesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.CurrencyId = _context.Currency.ToList();

            return View();
        }
        public IActionResult Upsert(int? id)
        {
            ViewBag.CurrencyId = _context.Currency.ToList();
            Branch branch = new Branch();
            if (id == null)
            {
                return View(branch);
            }
            branch = _context.Branch.Find(id.GetValueOrDefault());
            if (branch == null)
            {
                return NotFound();
            }
            return View(branch);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Branch branch)
        {
            if (ModelState.IsValid)
            {
                if (branch.Id == 0)
                {
                    _context.Branch.Add(branch);
                }
                else
                {
                    _context.Branch.Update(branch);
                }
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(branch);
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _context.Branch });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _context.Branch.Find(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }

            _context.Branch.Remove(objFromDb);
            _context.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful." });
        }


        #endregion
    }
}
