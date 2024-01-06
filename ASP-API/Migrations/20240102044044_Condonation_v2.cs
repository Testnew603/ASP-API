using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_API.Migrations
{
    public partial class Condonation_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"UPDATE CondonationFees SET Reason='none'");

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "CondonationFees",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "nil");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reason",
                table: "CondonationFees");
        }
    }
}
