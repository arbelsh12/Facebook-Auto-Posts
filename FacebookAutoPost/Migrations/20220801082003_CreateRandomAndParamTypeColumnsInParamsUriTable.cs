using Microsoft.EntityFrameworkCore.Migrations;

namespace FacebookAutoPost.Migrations
{
    public partial class CreateRandomAndParamTypeColumnsInParamsUriTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ParamType1",
                table: "ParamsUri",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParamType2",
                table: "ParamsUri",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParamType3",
                table: "ParamsUri",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RandomValue1",
                table: "ParamsUri",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RandomValue2",
                table: "ParamsUri",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RandomValue3",
                table: "ParamsUri",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParamType1",
                table: "ParamsUri");

            migrationBuilder.DropColumn(
                name: "ParamType2",
                table: "ParamsUri");

            migrationBuilder.DropColumn(
                name: "ParamType3",
                table: "ParamsUri");

            migrationBuilder.DropColumn(
                name: "RandomValue1",
                table: "ParamsUri");

            migrationBuilder.DropColumn(
                name: "RandomValue2",
                table: "ParamsUri");

            migrationBuilder.DropColumn(
                name: "RandomValue3",
                table: "ParamsUri");
        }
    }
}
