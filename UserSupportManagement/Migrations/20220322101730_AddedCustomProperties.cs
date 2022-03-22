using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserSupportManagement.Migrations
{
    public partial class AddedCustomProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConcernId",
                schema: "Identity",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCode",
                schema: "Identity",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EmployeeDOB",
                schema: "Identity",
                table: "User",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "EmployeeDepartment",
                schema: "Identity",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeDesignation",
                schema: "Identity",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeName",
                schema: "Identity",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Identity",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_User_ConcernId",
                schema: "Identity",
                table: "User",
                column: "ConcernId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Concerns_ConcernId",
                schema: "Identity",
                table: "User",
                column: "ConcernId",
                principalSchema: "Identity",
                principalTable: "Concerns",
                principalColumn: "ConcernId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Concerns_ConcernId",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_ConcernId",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ConcernId",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "EmployeeCode",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "EmployeeDOB",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "EmployeeDepartment",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "EmployeeDesignation",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "EmployeeName",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Identity",
                table: "User");
        }
    }
}
