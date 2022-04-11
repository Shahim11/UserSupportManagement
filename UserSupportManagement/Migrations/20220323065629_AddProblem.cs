using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserSupportManagement.Migrations
{
    public partial class AddProblem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Problems",
                schema: "Identity",
                columns: table => new
                {
                    ProblemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProblemTypeId = table.Column<int>(type: "int", nullable: false),
                    StatusTypeId = table.Column<int>(type: "int", nullable: false),
                    ProblemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProblemDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Problems", x => x.ProblemId);
                    table.ForeignKey(
                        name: "FK_Problems_ProblemTypes_ProblemTypeId",
                        column: x => x.ProblemTypeId,
                        principalSchema: "Identity",
                        principalTable: "ProblemTypes",
                        principalColumn: "ProblemTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Problems_StatusTypes_StatusTypeId",
                        column: x => x.StatusTypeId,
                        principalSchema: "Identity",
                        principalTable: "StatusTypes",
                        principalColumn: "StatusTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Problems_ProblemTypeId",
                schema: "Identity",
                table: "Problems",
                column: "ProblemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Problems_StatusTypeId",
                schema: "Identity",
                table: "Problems",
                column: "StatusTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Problems",
                schema: "Identity");
        }
    }
}