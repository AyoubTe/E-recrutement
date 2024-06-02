using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_recrutement.Migrations
{
    /// <inheritdoc />
    public partial class AddProfAndImgCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Profil",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "urlImageCompany",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Profil",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "urlImageCompany",
                table: "AspNetUsers");
        }
    }
}
