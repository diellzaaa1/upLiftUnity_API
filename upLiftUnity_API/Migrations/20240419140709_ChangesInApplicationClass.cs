using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace upLiftUnity_API.Migrations
{
    /// <inheritdoc />
    public partial class ChangesInApplicationClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MotivationLetter",
                table: "SupVol_Applications",
                newName: "ApplicationStatus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ApplicationStatus",
                table: "SupVol_Applications",
                newName: "MotivationLetter");
        }
    }
}
