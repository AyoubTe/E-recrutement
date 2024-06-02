using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_recrutement.Migrations
{
    /// <inheritdoc />
    public partial class addLogoCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateLine",
                value: new DateOnly(2024, 5, 9));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateLine",
                value: new DateOnly(2024, 5, 7));
        }
    }
}
