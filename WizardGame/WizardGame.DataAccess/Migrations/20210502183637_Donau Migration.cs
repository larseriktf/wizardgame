using Microsoft.EntityFrameworkCore.Migrations;

namespace WizardGame.DataAccess.Migrations
{
    public partial class DonauMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Configurations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConfigurationName = table.Column<string>(nullable: false),
                    Volume = table.Column<int>(nullable: false),
                    DisplayMode = table.Column<int>(nullable: false),
                    NavContinue = table.Column<string>(nullable: true),
                    NavPause = table.Column<string>(nullable: true),
                    NavBack = table.Column<string>(nullable: true),
                    MoveLeft = table.Column<string>(nullable: true),
                    MoveUp = table.Column<string>(nullable: true),
                    MoveRight = table.Column<string>(nullable: true),
                    MoveDown = table.Column<string>(nullable: true),
                    Action1 = table.Column<string>(nullable: true),
                    Action2 = table.Column<string>(nullable: true),
                    Action3 = table.Column<string>(nullable: true),
                    Action4 = table.Column<string>(nullable: true),
                    Interact1 = table.Column<string>(nullable: true),
                    Interact2 = table.Column<string>(nullable: true),
                    Interact3 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configurations", x => x.Id);
                });

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
            migrationBuilder.DropTable(
                name: "Configurations");
        }
    }
}
