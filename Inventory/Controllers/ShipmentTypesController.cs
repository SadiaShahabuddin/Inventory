using Microsoft.EntityFrameworkCore;
using Inventory.Data;
using Inventory.Models;
using Microsoft.AspNetCore.Mvc;


namespace Inventory.Controllers
{
    public class ShipmentTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShipmentTypesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Upsert(int? id)
        {
            ShipmentType shipmentType = new ShipmentType();
            if (id == null)
            {
                return View(shipmentType);
            }
            shipmentType = _context.ShipmentType.Find(id.GetValueOrDefault());
            if (shipmentType == null)
            {
                return NotFound();
            }
            return View(shipmentType);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ShipmentType shipmentType)
        {
            if (ModelState.IsValid)
            {
                if (shipmentType.ShipmentTypeId == 0)
                {
                    _context.ShipmentType.Add(shipmentType);
                }
                else
                {
                    _context.ShipmentType.Update(shipmentType);
                }
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shipmentType);
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _context.ShipmentType });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _context.ShipmentType.Find(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }

            _context.ShipmentType.Remove(objFromDb);
            _context.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful." });
        }


        #endregion
    }
}
