using Microsoft.EntityFrameworkCore.Migrations;

namespace WizardGame.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Configurations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Volume = table.Column<int>(nullable: false),
                    DisplayMode = table.Column<int>(nullable: false),
                    NavContinue = table.Column<string>(nullable: true),
                    NavPause = table.Column<string>(nullable: true),
                    NavBack = table.Column<string>(nullable: true),
                    MoveUp = table.Column<string>(nullable: true),
                    MoveLeft = table.Column<string>(nullable: true),
                    MoveDown = table.Column<string>(nullable: true),
                    MoveRight = table.Column<string>(nullable: true),
                    Action1 = table.Column<string>(nullable: true),
                    Action2 = table.Column<string>(nullable: true),
                    Action3 = table.Column<string>(nullable: true),
                    Action4 = table.Column<string>(nullable: true),
                    Interact1 = table.Column<string>(nullable: false),
                    Interact2 = table.Column<string>(nullable: false),
                    Interact3 = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configurations", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Configurations");
        }
    }
}
