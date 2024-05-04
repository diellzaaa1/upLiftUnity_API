using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace upLiftUnity_API.Migrations
{
    /// <inheritdoc />
    public partial class AddTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        


    

            migrationBuilder.CreateIndex(
                name: "IX_Calls_UserId",
                table: "Calls",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rules_UserId",
                table: "Rules",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calls");

            migrationBuilder.DropTable(
                name: "Rules");

            migrationBuilder.DropColumn(
                name: "ApplicationStatus",
                table: "SupVol_Applications");
        }
    }
}
