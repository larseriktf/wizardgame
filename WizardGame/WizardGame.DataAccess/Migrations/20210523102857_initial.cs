using Microsoft.EntityFrameworkCore.Migrations;

namespace WizardGame.DataAccess.Migrations
{
    public partial class initial : Migration
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

            migrationBuilder.CreateTable(
                name: "PlayerProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WavesPlayed = table.Column<int>(nullable: false),
                    EnemiesDefeated = table.Column<int>(nullable: false),
                    MinutesElapsed = table.Column<int>(nullable: false),
                    PlayerProfileId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameStatistics_PlayerProfiles_PlayerProfileId",
                        column: x => x.PlayerProfileId,
                        principalTable: "PlayerProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.InsertData(
                table: "GameStatistics",
                columns: new[] { "Id", "EnemiesDefeated", "MinutesElapsed", "PlayerProfileId", "WavesPlayed" },
                values: new object[,]
                {
                    { 9, 0, 0, null, 10 },
                    { 8, 0, 0, null, 10 },
                    { 7, 0, 0, null, 10 },
                    { 6, 0, 0, null, 10 },
                    { 3, 0, 0, null, 10 },
                    { 4, 0, 0, null, 10 },
                    { 2, 0, 0, null, 10 },
                    { 1, 0, 0, null, 10 },
                    { 5, 0, 0, null, 10 }
                });

            migrationBuilder.InsertData(
                table: "PlayerProfiles",
                columns: new[] { "Id", "PlayerName" },
                values: new object[,]
                {
                    { 2, "Patrenko Escobar" },
                    { 1, "Åge" },
                    { 3, "Player3" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameStatistics_PlayerProfileId",
                table: "GameStatistics",
                column: "PlayerProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Configurations");

            migrationBuilder.DropTable(
                name: "GameStatistics");

            migrationBuilder.DropTable(
                name: "PlayerProfiles");
        }
    }
}
