using Microsoft.EntityFrameworkCore.Migrations;

namespace UserSupportManagement.Migrations
{
    public partial class AddColumntoSolution : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "orderNeeded",
                schema: "Identity",
                table: "Solutions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "orderNeeded",
                schema: "Identity",
                table: "Solutions");
        }
    }
}
