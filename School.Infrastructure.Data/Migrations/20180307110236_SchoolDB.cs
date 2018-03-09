using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace School.Infrastructure.Data.Migrations
{
    public partial class SchoolDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Room",
                table: "Schedule");

            migrationBuilder.RenameColumn(
                name: "Data",
                table: "Schedule",
                newName: "Date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Schedule",
                newName: "Data");

            migrationBuilder.AddColumn<int>(
                name: "Room",
                table: "Schedule",
                nullable: true);
        }
    }
}
