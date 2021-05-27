using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WizardGame.DataAccess.Migrations
{
    public partial class RefactoredModel : Migration
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
                    PlayerName = table.Column<string>(nullable: false),
                    IsSelected = table.Column<bool>(nullable: false)
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
                    ElapsedTime = table.Column<TimeSpan>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameStatistics_PlayerProfiles_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "PlayerProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Configurations",
                columns: new[] { "Id", "Action1", "Action2", "Action3", "Action4", "ConfigurationName", "Interact1", "Interact2", "Interact3", "MoveDown", "MoveLeft", "MoveRight", "MoveUp", "NavBack", "NavContinue", "NavPause" },
                values: new object[,]
                {
                    { 1, "LEFTARROW", "UPARROW", "RIGHTARROW", "DOWNARROW", "Default Configuration", "R", "F", "C", "S", "A", "D", "W", "BACKSPACE", "ENTER", "ESCAPE" },
                    { 2, "A", "W", "D", "S", "Switch", "R", "F", "C", "UPARROW", "LEFTARROW", "RIGHTARROW", "DOWNARROW", "BACKSPACE", "ENTER", "ESCAPE" },
                    { 3, "LEFTARROW", "UPARROW", "RIGHTARROW", "DOWNARROW", "Windowed Borderless", "R", "F", "C", "S", "A", "D", "W", "BACKSPACE", "ENTER", "ESCAPE" },
                    { 4, "LEFTARROW", "UPARROW", "RIGHTARROW", "DOWNARROW", "Fullscreen", "R", "F", "C", "S", "A", "D", "W", "BACKSPACE", "ENTER", "ESCAPE" },
                    { 5, "NUMLEFT", "NUMUP", "NUMRIGHT", "NUMDOWN", "NumPad", "R", "F", "C", "S", "A", "D", "W", "BACKSPACE", "ENTER", "ESCAPE" },
                    { 6, "N", "J", "K", "M", "Close Together", "R", "F", "C", "S", "A", "D", "W", "BACKSPACE", "ENTER", "ESCAPE" }
                });

            migrationBuilder.InsertData(
                table: "PlayerProfiles",
                columns: new[] { "Id", "IsSelected", "PlayerName" },
                values: new object[,]
                {
                    { 1, false, "Åge" },
                    { 2, false, "Patrenko Escobar" },
                    { 3, false, "Player3" }
                });

            migrationBuilder.InsertData(
                table: "GameStatistics",
                columns: new[] { "Id", "ElapsedTime", "EnemiesDefeated", "PlayerId", "WavesPlayed" },
                values: new object[,]
                {
                    { 1, new TimeSpan(0, 0, 0, 0, 0), 0, 1, 1 },
                    { 2, new TimeSpan(0, 0, 0, 0, 0), 0, 1, 14 },
                    { 3, new TimeSpan(0, 0, 0, 0, 0), 0, 1, 29 },
                    { 4, new TimeSpan(0, 0, 0, 0, 0), 0, 1, 5 },
                    { 5, new TimeSpan(0, 0, 0, 0, 0), 0, 1, 15 },
                    { 6, new TimeSpan(0, 0, 0, 0, 0), 0, 2, 2 },
                    { 7, new TimeSpan(0, 0, 0, 0, 0), 0, 2, 40 },
                    { 8, new TimeSpan(0, 0, 0, 0, 0), 0, 2, 28 },
                    { 9, new TimeSpan(0, 0, 0, 0, 0), 0, 2, 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameStatistics_PlayerId",
                table: "GameStatistics",
                column: "PlayerId");
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
