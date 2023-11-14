using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory.Data.Migrations
{
    public partial class _0003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Vendor_VendorTypeId",
                table: "Vendor",
                column: "VendorTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendor_VendorType_VendorTypeId",
                table: "Vendor",
                column: "VendorTypeId",
                principalTable: "VendorType",
                principalColumn: "VendorTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendor_VendorType_VendorTypeId",
                table: "Vendor");

            migrationBuilder.DropIndex(
                name: "IX_Vendor_VendorTypeId",
                table: "Vendor");
        }
    }
}
