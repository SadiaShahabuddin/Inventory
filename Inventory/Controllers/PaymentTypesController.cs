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
    public class PaymentTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentTypesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Upsert(int? id)
        {
            PaymentType paymentType = new PaymentType();
            if (id == null)
            {
                return View(paymentType);
            }
            paymentType = _context.PaymentType.Find(id.GetValueOrDefault());
            if (paymentType == null)
            {
                return NotFound();
            }
            return View(paymentType);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(PaymentType paymentType)
        {
            if (ModelState.IsValid)
            {
                if (paymentType.PaymentTypeId == 0)
                {
                    _context.PaymentType.Add(paymentType);
                }
                else
                {
                    _context.PaymentType.Update(paymentType);
                }
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paymentType);
        }



        [HttpGet]

        public IActionResult GetAll()
        {
            return Json(new { data = _context.PaymentType });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _context.PaymentType.Find(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }

            _context.PaymentType.Remove(objFromDb);
            _context.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful." });
        }


       
    }
}
