using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SaatDegis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Hour",
                table: "Timetables",
                newName: "StartTime");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndTime",
                table: "Timetables",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "BreakDuration",
                table: "ScheduleSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LessonDuration",
                table: "ScheduleSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LunchBreakDuration",
                table: "ScheduleSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Timetables");

            migrationBuilder.DropColumn(
                name: "BreakDuration",
                table: "ScheduleSettings");

            migrationBuilder.DropColumn(
                name: "LessonDuration",
                table: "ScheduleSettings");

            migrationBuilder.DropColumn(
                name: "LunchBreakDuration",
                table: "ScheduleSettings");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Timetables",
                newName: "Hour");
        }
    }
}
