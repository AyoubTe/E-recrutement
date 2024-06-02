using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_recrutement.Migrations
{
    /// <inheritdoc />
    public partial class addLogoCompany1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlCompanyLogo",
                table: "Offers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 1,
                column: "UrlCompanyLogo",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlCompanyLogo",
                table: "Offers");
        }
    }
}
