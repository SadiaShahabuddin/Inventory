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
    public class SalesTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesTypesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Upsert(int? id)
        {
            SalesType salesType = new SalesType();
            if (id == null)
            {
                return View(salesType);
            }
            salesType = _context.SalesType.Find(id.GetValueOrDefault());
            if (salesType == null)
            {
                return NotFound();
            }
            return View(salesType);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(SalesType salesType)
        {
            if (ModelState.IsValid)
            {
                if (salesType.SalesTypeId == 0)
                {
                    _context.SalesType.Add(salesType);
                }
                else
                {
                    _context.SalesType.Update(salesType);
                }
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(salesType);
        }



        [HttpGet]

        public IActionResult GetAll()
        {
            return Json(new { data = _context.SalesType });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _context.SalesType.Find(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }

            _context.SalesType.Remove(objFromDb);
            _context.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful." });
        }



    }
}

