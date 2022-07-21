using Microsoft.EntityFrameworkCore.Migrations;

namespace FacebookAutoPost.Migrations
{
    public partial class CreateFrequencyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Frequency",
                columns: table => new
                {
                    PageId = table.Column<string>(type: "TEXT", nullable: false),
                    IsRandom = table.Column<bool>(type: "INTEGER", nullable: false),
                    Freaquency = table.Column<string>(type: "TEXT", nullable: true),
                    Cron = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frequency", x => x.PageId);
                });

            //migrationBuilder.CreateTable(
            //    name: "ParamsUri",
            //    columns: table => new
            //    {
            //        PageId = table.Column<string>(type: "TEXT", nullable: false),
            //        ParamOne = table.Column<string>(type: "TEXT", nullable: true),
            //        ParamTwo = table.Column<string>(type: "TEXT", nullable: true),
            //        ParamThree = table.Column<string>(type: "TEXT", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ParamsUri", x => x.PageId);
            //    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Frequency");

            //migrationBuilder.DropTable(
            //    name: "ParamsUri");
        }
    }
}
