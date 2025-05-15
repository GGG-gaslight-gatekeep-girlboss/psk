using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeShop.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddPayments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CardPayment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    IntentId = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedById = table.Column<string>(type: "text", nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedById = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardPayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardPayment_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CardPayment_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardPayment_CreatedById",
                table: "CardPayment",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_CardPayment_ModifiedById",
                table: "CardPayment",
                column: "ModifiedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardPayment");
        }
    }
}
