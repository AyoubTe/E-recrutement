using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_recrutement.Migrations
{
    /// <inheritdoc />
    public partial class ajoutDeMinMaxSalaire : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "maxSalary",
                table: "Offers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "minSalary",
                table: "Offers",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "maxSalary",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "minSalary",
                table: "Offers");

            migrationBuilder.InsertData(
                table: "Offers",
                columns: new[] { "Id", "Company", "DateLine", "DatePub", "Description", "Location", "Poste", "Profil", "Qualifications", "Remuneration", "Responsibilities", "Secteur", "Type", "UrlCompanyLogo", "rectuteurId" },
                values: new object[] { 1, "SAMITEC", new DateOnly(2024, 5, 9), new DateOnly(2024, 5, 25), "ghybfbjfiueghfyefnv dyy", "Casablanca", "Web Developer", 3, "FVUBYIZy dgyghf", "2335 - 4566$", "fuluihu fh", "Informatique", 1, null, "a997231b-4176-4538-a452-952056dd30e3" });
        }
    }
}
