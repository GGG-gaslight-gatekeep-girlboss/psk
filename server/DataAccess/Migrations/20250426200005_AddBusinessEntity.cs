using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeShop.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddBusinessEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EmployerBusinessId",
                table: "AspNetUsers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Business",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BusinessOwnerId = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedById = table.Column<string>(type: "text", nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedById = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Business", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Business_AspNetUsers_BusinessOwnerId",
                        column: x => x.BusinessOwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Business_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Business_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EmployerBusinessId",
                table: "AspNetUsers",
                column: "EmployerBusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Business_BusinessOwnerId",
                table: "Business",
                column: "BusinessOwnerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Business_CreatedById",
                table: "Business",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Business_ModifiedById",
                table: "Business",
                column: "ModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Business_EmployerBusinessId",
                table: "AspNetUsers",
                column: "EmployerBusinessId",
                principalTable: "Business",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Business_EmployerBusinessId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Business");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_EmployerBusinessId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EmployerBusinessId",
                table: "AspNetUsers");
        }
    }
}
