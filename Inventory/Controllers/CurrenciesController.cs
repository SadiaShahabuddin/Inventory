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
    public class CurrenciesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CurrenciesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Upsert(int? id)
        {
            Currency currency = new Currency();
            if (id == null)
            {
                return View(currency);
            }
            currency = _context.Currency.Find(id.GetValueOrDefault());
            if (currency == null)
            {
                return NotFound();
            }
            return View(currency);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Currency currency)
        {
            if (ModelState.IsValid)
            {
                if (currency.CurrencyId == 0)
                {
                    _context.Currency.Add(currency);
                }
                else
                {
                    _context.Currency.Update(currency);
                }
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(currency);
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _context.Currency });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _context.Currency.Find(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }

            _context.Currency.Remove(objFromDb);
            _context.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful." });
        }


        #endregion
    }
}
