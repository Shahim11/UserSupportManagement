using Microsoft.EntityFrameworkCore.Migrations;

namespace UserSupportManagement.Migrations
{
    public partial class AddColumnSolution : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderItemDetails",
                schema: "Identity",
                table: "Solutions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderItemName",
                schema: "Identity",
                table: "Solutions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderItemQuantity",
                schema: "Identity",
                table: "Solutions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderItemDetails",
                schema: "Identity",
                table: "Solutions");

            migrationBuilder.DropColumn(
                name: "OrderItemName",
                schema: "Identity",
                table: "Solutions");

            migrationBuilder.DropColumn(
                name: "OrderItemQuantity",
                schema: "Identity",
                table: "Solutions");
        }
    }
}
