using Microsoft.EntityFrameworkCore.Migrations;

namespace WizardGame.DataAccess.Migrations
{
    public partial class AddedIsSelectedTOPlayerProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSelected",
                table: "PlayerProfiles",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSelected",
                table: "PlayerProfiles");
        }
    }
}
