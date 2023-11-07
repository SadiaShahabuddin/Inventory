using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory.Data.Migrations
{
    public partial class ProductTableModify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Product",
                newName: "UnitOfMeasureId");

            migrationBuilder.AddColumn<string>(
                name: "Barcode",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "DefaultBuyingPrice",
                table: "Product",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DefaultSellingPrice",
                table: "Product",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "ProductCode",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductImageUrl",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Product_BranchId",
                table: "Product",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CurrencyId",
                table: "Product",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_UnitOfMeasureId",
                table: "Product",
                column: "UnitOfMeasureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Branch_BranchId",
                table: "Product",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Currency_CurrencyId",
                table: "Product",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_UnitOfMeasure_UnitOfMeasureId",
                table: "Product",
                column: "UnitOfMeasureId",
                principalTable: "UnitOfMeasure",
                principalColumn: "UnitOfMeasureId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Branch_BranchId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Currency_CurrencyId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_UnitOfMeasure_UnitOfMeasureId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_BranchId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_CurrencyId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_UnitOfMeasureId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Barcode",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "DefaultBuyingPrice",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "DefaultSellingPrice",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductCode",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductImageUrl",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "UnitOfMeasureId",
                table: "Product",
                newName: "Price");
        }
    }
}
