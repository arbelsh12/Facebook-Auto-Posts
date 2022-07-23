using Microsoft.EntityFrameworkCore.Migrations;

namespace FacebookAutoPost.Migrations
{
    public partial class RenameColumnFreaquency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Freaquency",
                table: "Frequency",
                newName: "PostFrequency");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostFrequency",
                table: "Frequency",
                newName: "Freaquency");
        }
    }
}
