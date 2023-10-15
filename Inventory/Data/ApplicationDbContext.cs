using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Inventory.Models;

namespace Inventory.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Inventory.Models.ProductType>? ProductType { get; set; }
        public DbSet<Inventory.Models.Product>? Product { get; set; }
        public DbSet<Inventory.Models.Category>? Category { get; set; }
        public DbSet<Inventory.Models.SubCategory>? SubCategory { get; set; }
        public DbSet<Inventory.Models.Brand>? Brand { get; set; }
        public DbSet<Inventory.Models.Supplier>? Supplier { get; set; }
        public DbSet<Inventory.Models.Vendor>? Vendor { get; set; }
        public DbSet<Inventory.Models.VendorType>? VendorType { get; set; }


    }
}