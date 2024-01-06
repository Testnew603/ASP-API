using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_API.Migrations
{
    public partial class CourseFee_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DBCC CHECKIDENT ('CourseFees', RESEED, 14)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
