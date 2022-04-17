using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FacebookAutoPost.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AutoPosts",
                columns: table => new
                {
                    PageId = table.Column<string>(type: "TEXT", nullable: false),
                    Token = table.Column<string>(type: "TEXT", nullable: true),
                    UserAPI = table.Column<string>(type: "TEXT", nullable: true),
                    PostTemplate = table.Column<string>(type: "TEXT", nullable: true),
                    Frequency = table.Column<string>(type: "TEXT", nullable: true),
                    Time = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ApiKey = table.Column<string>(type: "TEXT", nullable: true),  // nullable ??
                    Uri = table.Column<string>(type: "TEXT", nullable: true) // nullable ??
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoPosts", x => x.PageId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutoPosts");
        }
    }
}
