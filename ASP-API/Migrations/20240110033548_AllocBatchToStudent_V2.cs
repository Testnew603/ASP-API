using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_API.Migrations
{
    public partial class AllocBatchToStudent_V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "AllocBatchBranchToStudent");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AllocBatchBranchToStudent",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AllocBatchBranchToStudent");

            migrationBuilder.AddColumn<string>(
                name: "ModifiedAt",
                table: "AllocBatchBranchToStudent",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
