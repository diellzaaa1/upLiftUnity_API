using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace upLiftUnity_API.Migrations
{
    /// <inheritdoc />
    public partial class ScheduleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstSlot = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SecondSlot = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThirdDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThirdSlot = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FourthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FourthSlot = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedule_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_UserId",
                table: "Schedule",
                column: "UserId");
        }

        /// <inheritdoc />
       
    }
}
