using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CondonationFees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    FeeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FineAmount = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CondonationFees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CondonationFees_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentAgreement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    DomainId = table.Column<int>(type: "int", nullable: false),
                    CourseFeeID = table.Column<int>(type: "int", nullable: false),
                    CondonationFeeId = table.Column<int>(type: "int", nullable: false),
                    StartedAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndedAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Documents = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAgreement", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CondonationFees_StudentId",
                table: "CondonationFees",
                column: "StudentId");          
        }


        protected override void Down(MigrationBuilder migrationBuilder)
        {     

            migrationBuilder.DropTable(
                name: "CondonationFees");

            migrationBuilder.DropTable(
                name: "StudentAgreement");
        }
    }
}
