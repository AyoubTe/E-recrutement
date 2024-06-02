using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_recrutement.Migrations
{
    /// <inheritdoc />
    public partial class AddMotivationLettre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_AspNetUsers_ApplicationUserId",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_AspNetUsers_ApplicationUserId1",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_ApplicationUserId",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_ApplicationUserId1",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "Offers");

            migrationBuilder.CreateTable(
                name: "Candidatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CandidatId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfferId = table.Column<int>(type: "int", nullable: false),
                    MotivationLettre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateApplication = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidatures", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateLine",
                value: new DateOnly(2024, 5, 7));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candidatures");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Offers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "Offers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ApplicationUserId", "ApplicationUserId1", "DateLine" },
                values: new object[] { null, null, new DateOnly(2024, 5, 6) });

            migrationBuilder.CreateIndex(
                name: "IX_Offers_ApplicationUserId",
                table: "Offers",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_ApplicationUserId1",
                table: "Offers",
                column: "ApplicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_AspNetUsers_ApplicationUserId",
                table: "Offers",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_AspNetUsers_ApplicationUserId1",
                table: "Offers",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
