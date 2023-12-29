using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Data;
using Inventory.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }



        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            
            var usersWithBranchAndRole = _context.ApplicationUser
    .Include(u => u.Branch)
    .DefaultIfEmpty()
    .ToList();


            var userData = usersWithBranchAndRole.Select(user => new
            {
                Id = user.Id,
                Name = user.Name,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                BranchName = user.Branch?.BranchName,
                Role = GetUserRoles(user.Id)
            });

            return Json(new { data = userData });
        }
        private List<string> GetUserRoles(string userId)
        {
            var userRoles = _userManager.GetRolesAsync(_userManager.FindByIdAsync(userId).Result).Result;
            return userRoles.ToList();
        }

        [HttpPost]
        public IActionResult LockUnlock([FromBody] string id)
        {
            var objFromDb = _context.ApplicationUser.FirstOrDefault(u => u.Id == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while Locking/Unlocking" });
            }
            if (objFromDb.LockoutEnd != null && objFromDb.LockoutEnd > DateTime.Now)
            {
                objFromDb.LockoutEnd = DateTime.Now;
            }
            else
            {
                objFromDb.LockoutEnd = DateTime.Now.AddYears(100);
            }
            _context.SaveChangesAsync();


            return Json(new { success = true, message = "Operation Successful." });
        }

    }
}