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
    public class UnitOfMeasuresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UnitOfMeasuresController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Upsert(int? id)
        {
            UnitOfMeasure unitOfMeasure = new UnitOfMeasure();
            if (id == null)
            {
                return View(unitOfMeasure);
            }
            unitOfMeasure = _context.UnitOfMeasure.Find(id.GetValueOrDefault());
            if (unitOfMeasure == null)
            {
                return NotFound();
            }
            return View(unitOfMeasure);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(UnitOfMeasure unitOfMeasure)
        {
            if (ModelState.IsValid)
            {
                if (unitOfMeasure.UnitOfMeasureId == 0)
                {
                    _context.UnitOfMeasure.Add(unitOfMeasure);
                }
                else
                {
                    _context.UnitOfMeasure.Update(unitOfMeasure);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(unitOfMeasure);
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _context.UnitOfMeasure });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var objFromDb = _context.UnitOfMeasure.Find(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }

            _context.UnitOfMeasure.Remove(objFromDb);
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful." });
        }
        #endregion
    }
}
