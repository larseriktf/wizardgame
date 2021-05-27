using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WizardGame.DataAccess.Migrations
{
    public partial class ElapsedTimeAsTimeSpan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinutesElapsed",
                table: "GameStatistics");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ElapsedTime",
                table: "GameStatistics",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ElapsedTime",
                table: "GameStatistics");

            migrationBuilder.AddColumn<int>(
                name: "MinutesElapsed",
                table: "GameStatistics",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
