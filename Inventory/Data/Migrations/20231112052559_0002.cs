using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory.Data.Migrations
{
    public partial class _0002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "PurchaseOrder",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_BranchId",
                table: "PurchaseOrder",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_CurrencyId",
                table: "PurchaseOrder",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_PurchaseTypeId",
                table: "PurchaseOrder",
                column: "PurchaseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_VendorId",
                table: "PurchaseOrder",
                column: "VendorId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrder_Branch_BranchId",
                table: "PurchaseOrder",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrder_Currency_CurrencyId",
                table: "PurchaseOrder",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrder_PurchaseType_PurchaseTypeId",
                table: "PurchaseOrder",
                column: "PurchaseTypeId",
                principalTable: "PurchaseType",
                principalColumn: "PurchaseTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrder_Vendor_VendorId",
                table: "PurchaseOrder",
                column: "VendorId",
                principalTable: "Vendor",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrder_Branch_BranchId",
                table: "PurchaseOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrder_Currency_CurrencyId",
                table: "PurchaseOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrder_PurchaseType_PurchaseTypeId",
                table: "PurchaseOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrder_Vendor_VendorId",
                table: "PurchaseOrder");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrder_BranchId",
                table: "PurchaseOrder");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrder_CurrencyId",
                table: "PurchaseOrder");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrder_PurchaseTypeId",
                table: "PurchaseOrder");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrder_VendorId",
                table: "PurchaseOrder");

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "PurchaseOrder",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
