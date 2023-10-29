using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.MainMenu
{
    public static class MainMenu
    {
        public static class Customer
        {
            public const string PageName = "Customer";
            public const string RoleName = "Customer";
            public const string Path = "/Customers/Index";
            public const string ControllerName = "Customers";
            public const string ActionName = "Index";
        }

        public static class Vendor
        {
            public const string PageName = "Vendor";
            public const string RoleName = "Vendor";
            public const string Path = "/Vendors/Index";
            public const string ControllerName = "Vendors";
            public const string ActionName = "Index";
        }

        public static class Product
        {
            public const string PageName = "Product";
            public const string RoleName = "Product";
            public const string Path = "/Products/Index";
            public const string ControllerName = "Products";
            public const string ActionName = "Index";
        }

        public static class PurchaseOrder
        {
            public const string PageName = "Purchase Order";
            public const string RoleName = "Purchase Order";
            public const string Path = "/PurchaseOrders/Index";
            public const string ControllerName = "PurchaseOrdes";
            public const string ActionName = "Index";
        }

        public static class GoodsReceivedNote
        {
            public const string PageName = "Goods Received Note";
            public const string RoleName = "Goods Received Note";
            public const string Path = "/GoodsReceivedNotes/Index";
            public const string ControllerName = "GoodsReceivedNotes";
            public const string ActionName = "Index";
        }

        public static class Bill
        {
            public const string PageName = "Bill";
            public const string RoleName = "Bill";
            public const string Path = "/Bills/Index";
            public const string ControllerName = "Bills";
            public const string ActionName = "Index";
        }

        public static class PaymentVoucher
        {
            public const string PageName = "Payment Voucher";
            public const string RoleName = "Payment Voucher";
            public const string Path = "/PaymentVouchers/Index";
            public const string ControllerName = "PaymentVouchers";
            public const string ActionName = "Index";
        }

        public static class SalesOrder
        {
            public const string PageName = "Sales Order";
            public const string RoleName = "Sales Order";
            public const string Path = "/SalesOrders/Index";
            public const string ControllerName = "SalesOrders";
            public const string ActionName = "Index";
        }

        public static class Shipment
        {
            public const string PageName = "Shipment";
            public const string RoleName = "Shipment";
            public const string Path = "/Shipments/Index";
            public const string ControllerName = "Shipments";
            public const string ActionName = "Index";
        }

        public static class Invoice
        {
            public const string PageName = "Invoice";
            public const string RoleName = "Invoice";
            public const string Path = "/Invoices/Index";
            public const string ControllerName = "Invoices";
            public const string ActionName = "Index";
        }

        public static class PaymentReceive
        {
            public const string PageName = "Payment Receive";
            public const string RoleName = "Payment Receive";
            public const string Path = "/PaymentReceives/Index";
            public const string ControllerName = "PaymentReceives";
            public const string ActionName = "Index";
        }

        public static class BillType
        {
            public const string PageName = "Bill Type";
            public const string RoleName = "Bill Type";
            public const string Path = "/BillTypes/Index";
            public const string ControllerName = "BillTypes";
            public const string ActionName = "Index";
        }

        public static class Branch
        {
            public const string PageName = "Branch";
            public const string RoleName = "Branch";
            public const string Path = "/Branches/Index";
            public const string ControllerName = "Branches";
            public const string ActionName = "Index";
        }

        public static class CashBank
        {
            public const string PageName = "Cash Bank";
            public const string RoleName = "Cash Bank";
            public const string Path = "/CashBanks/Index";
            public const string ControllerName = "CashBanks";
            public const string ActionName = "Index";
        }

        public static class Currency
        {
            public const string PageName = "Currency";
            public const string RoleName = "Currency";
            public const string Path = "/Currencies/Index";
            public const string ControllerName = "Currencies";
            public const string ActionName = "Index";
        }

        public static class CustomerType
        {
            public const string PageName = "Customer Type";
            public const string RoleName = "Customer Type";
            public const string Path = "/CustomerTypes/Index";
            public const string ControllerName = "CustomerTypes";
            public const string ActionName = "Index";
        }

        public static class InvoiceType
        {
            public const string PageName = "Invoice Type";
            public const string RoleName = "Invoice Type";
            public const string Path = "/InvoiceTypes/Index";
            public const string ControllerName = "InvoiceTypes";
            public const string ActionName = "Index";
        }

        public static class PaymentType
        {
            public const string PageName = "Payment Type";
            public const string RoleName = "Payment Type";
            public const string Path = "/PaymentTypes/Index";
            public const string ControllerName = "PaymentTypes";
            public const string ActionName = "Index";
        }

        public static class ProductType
        {
            public const string PageName = "Product Type";
            public const string RoleName = "Product Type";
            public const string Path = "/ProductTypes/Index";
            public const string ControllerName = "ProductTypes";
            public const string ActionName = "Index";
        }

        public static class SalesType
        {
            public const string PageName = "Sales Type";
            public const string RoleName = "Sales Type";
            public const string Path = "/SalesTypes/Index";
            public const string ControllerName = "SalesTypes";
            public const string ActionName = "Index";
        }

        public static class ShipmentType
        {
            public const string PageName = "Shipment Type";
            public const string RoleName = "Shipment Type";
            public const string Path = "/ShipmentTypes/Index";
            public const string ControllerName = "ShipmentTypes";
            public const string ActionName = "Index";
        }

        public static class UnitOfMeasure
        {
            public const string PageName = "Unit Of Measure";
            public const string RoleName = "Unit Of Measure";
            public const string Path = "/UnitOfMeasures/Index";
            public const string ControllerName = "UnitOfMeasures";
            public const string ActionName = "Index";
        }

        public static class VendorType
        {
            public const string PageName = "Vendor Type";
            public const string RoleName = "Vendor Type";
            public const string Path = "/VendorTypes/Index";
            public const string ControllerName = "VendorTypes";
            public const string ActionName = "Index";
        }

        public static class Warehouse
        {
            public const string PageName = "Warehouse";
            public const string RoleName = "Warehouse";
            public const string Path = "/Warehouses/Index";
            public const string ControllerName = "Warehouses";
            public const string ActionName = "Index";
        }

        public static class PurchaseType
        {
            public const string PageName = "Purchase Type";
            public const string RoleName = "Purchase Type";
            public const string Path = "/PurchaseTypes/Index";
            public const string ControllerName = "PurchaseTypes";
            public const string ActionName = "Index";
        }

        public static class User
        {
            public const string PageName = "User";
            public const string RoleName = "User";
            public const string Path = "/UserRoles/Index";
            public const string ControllerName = "UserRoles";
            public const string ActionName = "Index";
        }

        public static class ChangePassword
        {
            public const string PageName = "Change Password";
            public const string RoleName = "Change Password";
            public const string Path = "/UserRoles/ChangePassword";
            public const string ControllerName = "UserRoles";
            public const string ActionName = "ChangePassword";
        }

        public static class Role
        {
            public const string PageName = "Role";
            public const string RoleName = "Role";
            public const string Path = "/UserRoles/Role";
            public const string ControllerName = "UserRoles";
            public const string ActionName = "Role";
        }

        public static class ChangeRole
        {
            public const string PageName = "Change Role";
            public const string RoleName = "Change Role";
            public const string Path = "/UserRoles/ChangeRole";
            public const string ControllerName = "UserRoles";
            public const string ActionName = "ChangeRole";
        }

        public static class Dashboard
        {
            public const string PageName = "Dashboard Main";
            public const string RoleName = "Dashboard Main";
            public const string Path = "/Home/Index";
            public const string ControllerName = "Home";
            public const string ActionName = "Index";
        }

    }
}
