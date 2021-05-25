using Microsoft.EntityFrameworkCore.Migrations;

namespace WizardGame.DataAccess.Migrations
{
    public partial class AddedINotifyPropertyChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayMode",
                table: "Configurations");

            migrationBuilder.DropColumn(
                name: "Volume",
                table: "Configurations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DisplayMode",
                table: "Configurations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Volume",
                table: "Configurations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 1,
                column: "Volume",
                value: 50);

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 2,
                column: "Volume",
                value: 50);

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DisplayMode", "Volume" },
                values: new object[] { 1, 50 });

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DisplayMode", "Volume" },
                values: new object[] { 2, 50 });

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 5,
                column: "Volume",
                value: 50);

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 6,
                column: "Volume",
                value: 50);
        }
    }
}
