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
        
        public DbSet<Inventory.Models.Brand>? Brand { get; set; }
        public DbSet<Inventory.Models.Category>? Category { get; set; }
        public DbSet<Inventory.Models.Product>? Product { get; set; }
        public DbSet<Inventory.Models.ProductType>? ProductType { get; set; }
        public DbSet<Inventory.Models.SubCategory>? SubCategory { get; set; }
        public DbSet<Inventory.Models.Vendor>? Vendor { get; set; }
        public DbSet<Inventory.Models.VendorType>? VendorType { get; set; }
        public DbSet<Inventory.Models.Branch>? Branch { get; set; }
        public DbSet<Inventory.Models.PaymentType>? PaymentType { get; set; }
        public DbSet<Inventory.Models.Currency>? Currency { get; set; }
        public DbSet<Inventory.Models.CashBank>? CashBank { get; set; }
        public DbSet<Inventory.Models.Customer>? Customer { get; set; }
        public DbSet<Inventory.Models.CustomerType>? CustomerType { get; set; }
        public DbSet<Inventory.Models.InvoiceType>? InvoiceType { get; set; }
        public DbSet<Inventory.Models.SalesType>? SalesType { get; set; }
        public DbSet<Inventory.Models.UnitOfMeasure>? UnitOfMeasure { get; set; }
        public DbSet<Inventory.Models.Warehouse>? Warehouse { get; set; }
        public DbSet<Inventory.Models.BillType>? BillType { get; set; }
        public DbSet<Inventory.Models.PurchaseType>? PurchaseType { get; set; }
        public DbSet<Inventory.Models.ShipmentType>? ShipmentType { get; set; }
        public DbSet<Inventory.Models.PurchaseOrder>? PurchaseOrder { get; set; }
        public DbSet<Inventory.Models.PurchaseOrderLine>? PurchaseOrderLine { get; set; }
        public DbSet<Inventory.Models.SalesOrder>? SalesOrder { get; set; }
        public DbSet<Inventory.Models.SalesOrderLine>? SalesOrderLine { get; set; }
    }
}