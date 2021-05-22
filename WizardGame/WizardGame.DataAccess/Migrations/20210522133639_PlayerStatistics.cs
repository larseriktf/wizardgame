using Microsoft.EntityFrameworkCore.Migrations;

namespace WizardGame.DataAccess.Migrations
{
    public partial class PlayerStatistics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                table: "GameStatistics",
                columns: new[] { "Id", "EnemiesDefeated", "MinutesElapsed", "PlayerProfileId", "WavesPlayed" },
                values: new object[,]
                {
                    { 1, 0, 0, null, 10 },
                    { 2, 0, 0, null, 10 },
                    { 3, 0, 0, null, 10 },
                    { 4, 0, 0, null, 10 },
                    { 5, 0, 0, null, 10 },
                    { 6, 0, 0, null, 10 },
                    { 7, 0, 0, null, 10 },
                    { 8, 0, 0, null, 10 },
                    { 9, 0, 0, null, 10 }
                });

            migrationBuilder.InsertData(
                table: "PlayerProfiles",
                columns: new[] { "Id", "PlayerName" },
                values: new object[,]
                {
                    { 1, "Åge" },
                    { 2, "Patrenko Escobar" },
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
                name: "GameStatistics");

            migrationBuilder.DropTable(
                name: "PlayerProfiles");
        }
    }
}
