using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_API.Migrations
{
    public partial class twoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Domains",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MainDomain = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubDomain = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "studentDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Qualification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Documents = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DomainId = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Profile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_studentDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_studentDetails_Domains_DomainId",
                        column: x => x.DomainId,
                        principalTable: "Domains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Domains",
                columns: new[] { "Id", "MainDomain", "SubDomain" },
                values: new object[] { 1, "Frontend", "Angular" });

            migrationBuilder.InsertData(
                table: "studentDetails",
                columns: new[] { "Id", "Address", "BirthDate", "Documents", "DomainId", "Email", "FirstName", "Gender", "LastName", "Mobile", "Password", "Profile", "Qualification", "Status" },
                values: new object[] { 1, "Calicut", "1996-03-19", "nil", 1, "Abhijith@gmail.com", "Abhijith", "male", "K", "9876454423", "abhijith123", "nil", "BCA", "PENDING" });

            migrationBuilder.CreateIndex(
                name: "IX_studentDetails_DomainId",
                table: "studentDetails",
                column: "DomainId");

            migrationBuilder.RenameTable(
           name: "studentDetails",
           schema: "dbo",
           newName: "Students",
           newSchema: "dbo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "studentDetails");

            migrationBuilder.DropTable(
                name: "Domains");

             migrationBuilder.RenameTable(
           name: "Students",
           schema: "dbo",
           newName: "studentDetails",
           newSchema: "dbo");
        }
    }
}
