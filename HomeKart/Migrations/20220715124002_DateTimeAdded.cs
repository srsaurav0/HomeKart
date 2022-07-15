using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeKart.Migrations
{
    public partial class DateTimeAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeCreater",
                table: "Registers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTimeCreater",
                table: "Registers");
        }
    }
}
