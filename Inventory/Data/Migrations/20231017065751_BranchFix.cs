using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory.Data.Migrations
{
    public partial class BranchFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PaymentType",
                newName: "PaymentTypeId");

            migrationBuilder.RenameColumn(
                name: "BranchId",
                table: "Branch",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentTypeId",
                table: "PaymentType",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Branch",
                newName: "BranchId");
        }
    }
}
