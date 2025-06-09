using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapstoneProject_1.Migrations
{
    /// <inheritdoc />
    public partial class athletesubscrition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AthleteName",
                table: "AthleteSubscriptionDataDTOs",
                newName: "Intensity");

            migrationBuilder.AddColumn<string>(
                name: "CoachName",
                table: "AthleteSubscriptionDataDTOs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AthleteSubscriptionDataDTOs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Duration",
                table: "AthleteSubscriptionDataDTOs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "feedbackdto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CoachId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "TrainingProgramsdto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CoachId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Intensity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "feedbackdto");

            migrationBuilder.DropTable(
                name: "TrainingProgramsdto");

            migrationBuilder.DropColumn(
                name: "CoachName",
                table: "AthleteSubscriptionDataDTOs");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "AthleteSubscriptionDataDTOs");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "AthleteSubscriptionDataDTOs");

            migrationBuilder.RenameColumn(
                name: "Intensity",
                table: "AthleteSubscriptionDataDTOs",
                newName: "AthleteName");
        }
    }
}
