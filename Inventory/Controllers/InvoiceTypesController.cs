using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Inventory.Data;
using Inventory.Models;
using Microsoft.AspNetCore.Authorization;

namespace Inventory.Controllers
{
    [Authorize]

    public class InvoiceTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoiceTypesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Upsert(int? id)
        {
            InvoiceType invoiceType = new InvoiceType();
            //create
            if (id == null)
            {
                return View(invoiceType);
            }
            //when Update
            invoiceType = _context.InvoiceType.Find(id.GetValueOrDefault());
            if (invoiceType == null)
            {
                return NotFound();
            }
            return View(invoiceType);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(InvoiceType invoiceType)
        {
            if (ModelState.IsValid)
            {
                if (invoiceType.InvoiceTypeId == 0)
                {
                    _context.InvoiceType.Add(invoiceType);
                }
                else
                {
                    _context.InvoiceType.Update(invoiceType);
                }
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(invoiceType);
        }



        [HttpGet]

        public IActionResult GetAll()
        {
            return Json(new { data = _context.InvoiceType });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _context.InvoiceType.Find(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }

            _context.InvoiceType.Remove(objFromDb);
            _context.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful." });
        }



    }
}
