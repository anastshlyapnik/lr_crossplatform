using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shlyapnikova_lr.Migrations
{
    /// <inheritdoc />
    public partial class UpdConnections2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Volunteer_VolunteerId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_VolunteerId",
                table: "Student");

            migrationBuilder.AddColumn<string>(
                name: "StudentIds",
                table: "Volunteer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentIds",
                table: "Volunteer");

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
    }
}
