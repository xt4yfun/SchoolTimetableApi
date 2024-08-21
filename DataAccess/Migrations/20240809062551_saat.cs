using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class saat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // İlk olarak geçici bir sütun ekleyelim
            migrationBuilder.AddColumn<TimeSpan>(
                name: "HourTemp",
                table: "Timetables",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0));

            // Var olan int sütunundaki değerleri yeni sütuna dönüştürerek aktaralım
            migrationBuilder.Sql(
                "UPDATE Timetables SET HourTemp = DATEADD(HOUR, Hour, 0)");

            // Eski sütunu kaldırıp, yeni sütunun adını eski sütun adıyla değiştirelim
            migrationBuilder.DropColumn(
                name: "Hour",
                table: "Timetables");

            migrationBuilder.RenameColumn(
                name: "HourTemp",
                table: "Timetables",
                newName: "Hour");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Geri dönüş işlemi için eski veri tipini geri ekleyelim
            migrationBuilder.AddColumn<int>(
                name: "HourInt",
                table: "Timetables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // Mevcut TimeSpan değerlerini int olarak dönüştürelim
            migrationBuilder.Sql(
                "UPDATE Timetables SET HourInt = DATEPART(HOUR, Hour)");

            // Yeni sütunu kaldırıp, eski sütunun adını geri döndürelim
            migrationBuilder.DropColumn(
                name: "Hour",
                table: "Timetables");

            migrationBuilder.RenameColumn(
                name: "HourInt",
                table: "Timetables",
                newName: "Hour");
        }
    }
}
