using Microsoft.EntityFrameworkCore.Migrations;

namespace UserSupportManagement.Migrations
{
    public partial class AddIsDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Identity",
                table: "Vendors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Identity",
                table: "StatusTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Identity",
                table: "Solutions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Identity",
                table: "ProblemTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Identity",
                table: "Problems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Identity",
                table: "Concerns",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Identity",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Identity",
                table: "StatusTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Identity",
                table: "Solutions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Identity",
                table: "ProblemTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Identity",
                table: "Problems");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Identity",
                table: "Concerns");
        }
    }
}
