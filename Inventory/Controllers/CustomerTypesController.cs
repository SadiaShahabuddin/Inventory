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
    public class CustomerTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerTypesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Upsert(int? id)
        {
            CustomerType customerType = new CustomerType();
            if (id == null)
            {
                return View(customerType);
            }
            customerType = _context.CustomerType.Find(id.GetValueOrDefault());
            if (customerType == null)
            {
                return NotFound();
            }
            return View(customerType);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CustomerType customerType)
        {
            if (ModelState.IsValid)
            {
                if (customerType.CustomerTypeId == 0)
                {
                    _context.CustomerType.Add(customerType);
                }
                else
                {
                    _context.CustomerType.Update(customerType);
                }
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customerType);
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _context.CustomerType });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _context.CustomerType.Find(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }

            _context.CustomerType.Remove(objFromDb);
            _context.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful." });
        }


        #endregion
    }
}
