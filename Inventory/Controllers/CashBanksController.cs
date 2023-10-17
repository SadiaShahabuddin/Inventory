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
    public class CashBanksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CashBanksController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Upsert(int? id)
        {
            CashBank cashBank = new CashBank();
            if (id == null)
            {
                return View(cashBank);
            }
            cashBank = _context.CashBank.Find(id.GetValueOrDefault());
            if (cashBank == null)
            {
                return NotFound();
            }
            return View(cashBank);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CashBank cashBank)
        {
            if (ModelState.IsValid)
            {
                if (cashBank.CashBankId == 0)
                {
                    _context.CashBank.Add(cashBank);
                }
                else
                {
                    _context.CashBank.Update(cashBank);
                }
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cashBank);
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _context.CashBank });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _context.CashBank.Find(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }

            _context.CashBank.Remove(objFromDb);
            _context.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful." });
        }


        #endregion
    }
}
