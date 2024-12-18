using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shlyapnikova_lr.Migrations
{
    /// <inheritdoc />
    public partial class UpdConnections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VolunteerId",
                table: "Student",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Student_VolunteerId",
                table: "Student",
                column: "VolunteerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Volunteer_VolunteerId",
                table: "Student",
                column: "VolunteerId",
                principalTable: "Volunteer",
                principalColumn: "VolunteerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Volunteer_VolunteerId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_VolunteerId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "VolunteerId",
                table: "Student");
        }
    }
}
