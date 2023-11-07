using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory.Data.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Product_BranchId",
                table: "Product",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CustomerTypeId",
                table: "Customer",
                column: "CustomerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_CurrencyId",
                table: "Branch",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Branch_Currency_CurrencyId",
                table: "Branch",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_CustomerType_CustomerTypeId",
                table: "Customer",
                column: "CustomerTypeId",
                principalTable: "CustomerType",
                principalColumn: "CustomerTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Branch_BranchId",
                table: "Product",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branch_Currency_CurrencyId",
                table: "Branch");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_CustomerType_CustomerTypeId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Branch_BranchId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_BranchId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Customer_CustomerTypeId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Branch_CurrencyId",
                table: "Branch");
        }
    }
}
