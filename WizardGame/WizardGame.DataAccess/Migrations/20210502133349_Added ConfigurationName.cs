using Microsoft.EntityFrameworkCore.Migrations;

namespace WizardGame.DataAccess.Migrations
{
    public partial class AddedConfigurationName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Interact3",
                table: "Configurations",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)");

            migrationBuilder.AlterColumn<string>(
                name: "Interact2",
                table: "Configurations",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)");

            migrationBuilder.AlterColumn<string>(
                name: "Interact1",
                table: "Configurations",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)");

            migrationBuilder.AddColumn<string>(
                name: "ConfigurationName",
                table: "Configurations",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Configurations",
                columns: new[] { "Id", "Action1", "Action2", "Action3", "Action4", "ConfigurationName", "DisplayMode", "Interact1", "Interact2", "Interact3", "MoveDown", "MoveLeft", "MoveRight", "MoveUp", "NavBack", "NavContinue", "NavPause", "Volume" },
                values: new object[,]
                {
                    { 1, "LEFTARROW", "UPARROW", "RIGHTARROW", "DOWNARROW", "Default Configuration", 0, "R", "F", "C", "S", "A", "D", "W", "BACKSPACE", "ENTER", "ESCAPE", 50 },
                    { 2, "A", "W", "D", "S", "Switch", 0, "R", "F", "C", "UPARROW", "LEFTARROW", "RIGHTARROW", "DOWNARROW", "BACKSPACE", "ENTER", "ESCAPE", 50 },
                    { 3, "LEFTARROW", "UPARROW", "RIGHTARROW", "DOWNARROW", "Windowed Borderless", 1, "R", "F", "C", "S", "A", "D", "W", "BACKSPACE", "ENTER", "ESCAPE", 50 },
                    { 4, "LEFTARROW", "UPARROW", "RIGHTARROW", "DOWNARROW", "Fullscreen", 2, "R", "F", "C", "S", "A", "D", "W", "BACKSPACE", "ENTER", "ESCAPE", 50 },
                    { 5, "NUMLEFT", "NUMUP", "NUMRIGHT", "NUMDOWN", "NumPad", 0, "R", "F", "C", "S", "A", "D", "W", "BACKSPACE", "ENTER", "ESCAPE", 50 },
                    { 6, "N", "J", "K", "M", "Close Together", 0, "R", "F", "C", "S", "A", "D", "W", "BACKSPACE", "ENTER", "ESCAPE", 50 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DropColumn(
                name: "ConfigurationName",
                table: "Configurations");

            migrationBuilder.AlterColumn<string>(
                name: "Interact3",
                table: "Configurations",
                type: "nvarchar(1)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Interact2",
                table: "Configurations",
                type: "nvarchar(1)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Interact1",
                table: "Configurations",
                type: "nvarchar(1)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
