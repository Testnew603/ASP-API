using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_API.Migrations
{
    public partial class AddBatchesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
       name: "Batches",
       columns: table => new
       {
           BatchId = table.Column<int>(type: "int", nullable: false)
               .Annotation("SqlServer:Identity", "1, 1"),
           BatchName = table.Column<string>(type: "nvarchar(max)", nullable: true)
       },
       constraints: table =>
       {
           table.PrimaryKey("PK_Batches", x => x.BatchId);
       });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
