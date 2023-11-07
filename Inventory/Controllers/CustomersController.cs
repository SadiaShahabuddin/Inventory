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
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Upsert(int? id)
        {
            Customer customer = new Customer();
            if (id == null)
            {
                return View(customer);
            }
            customer = _context.Customer.Find(id.GetValueOrDefault());
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (customer.CustomerId == 0)
                {
                    _context.Customer.Add(customer);
                }
                else
                {
                    _context.Customer.Update(customer);
                }
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(customer);
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _context.Customer });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _context.Customer.Find(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }

            _context.Customer.Remove(objFromDb);
            _context.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful." });
        }


        #endregion
    }
}
