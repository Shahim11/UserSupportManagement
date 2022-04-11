using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserSupportManagement.Migrations
{
    public partial class AddOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "Identity",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorId = table.Column<int>(type: "int", nullable: false),
                    StatusTypeId = table.Column<int>(type: "int", nullable: false),
                    ProblemId = table.Column<int>(type: "int", nullable: false),
                    OrderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderItemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderItemDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderItemQuantity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Problems_ProblemId",
                        column: x => x.ProblemId,
                        principalSchema: "Identity",
                        principalTable: "Problems",
                        principalColumn: "ProblemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_StatusTypes_StatusTypeId",
                        column: x => x.StatusTypeId,
                        principalSchema: "Identity",
                        principalTable: "StatusTypes",
                        principalColumn: "StatusTypeId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Orders_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalSchema: "Identity",
                        principalTable: "Vendors",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProblemId",
                schema: "Identity",
                table: "Orders",
                column: "ProblemId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StatusTypeId",
                schema: "Identity",
                table: "Orders",
                column: "StatusTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_VendorId",
                schema: "Identity",
                table: "Orders",
                column: "VendorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders",
                schema: "Identity");
        }
    }
}