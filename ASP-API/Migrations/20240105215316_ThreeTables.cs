using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_API.Migrations
{
    public partial class ThreeTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AllocBatchBranchToStudent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    BatchId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    ModifiedAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllocBatchBranchToStudent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AllocBatchBranchToStudent_Batches_BatchId",
                        column: x => x.BatchId,
                        principalTable: "Batches",
                        principalColumn: "BatchId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AllocBatchBranchToStudent_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AllocBatchBranchToStudent_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AllocStudentToAdvisor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    AdvisorId = table.Column<int>(type: "int", nullable: false),
                    BatchId = table.Column<int>(type: "int", nullable: false),
                    DomainId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllocStudentToAdvisor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AllocStudentToAdvisor_Advisors_AdvisorId",
                        column: x => x.AdvisorId,
                        principalTable: "Advisors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AllocStudentToAdvisor_Batches_BatchId",
                        column: x => x.BatchId,
                        principalTable: "Batches",
                        principalColumn: "BatchId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AllocStudentToAdvisor_Domains_DomainId",
                        column: x => x.DomainId,
                        principalTable: "Domains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AllocStudentToAdvisor_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "BatchToAdvisors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdvisorId = table.Column<int>(type: "int", nullable: false),
                    BatchId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchToAdvisors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BatchToAdvisors_Advisors_AdvisorId",
                        column: x => x.AdvisorId,
                        principalTable: "Advisors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_BatchToAdvisors_Batches_BatchId",
                        column: x => x.BatchId,
                        principalTable: "Batches",
                        principalColumn: "BatchId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Fees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    AdvisorId = table.Column<int>(type: "int", nullable: false),
                    SpaceRent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaidAmount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Balance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaidThrough = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fees_Advisors_AdvisorId",
                        column: x => x.AdvisorId,
                        principalTable: "Advisors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Fees_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllocBatchBranchToStudent_BatchId",
                table: "AllocBatchBranchToStudent",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "IX_AllocBatchBranchToStudent_BranchId",
                table: "AllocBatchBranchToStudent",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_AllocBatchBranchToStudent_StudentId",
                table: "AllocBatchBranchToStudent",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_AllocStudentToAdvisor_AdvisorId",
                table: "AllocStudentToAdvisor",
                column: "AdvisorId");

            migrationBuilder.CreateIndex(
                name: "IX_AllocStudentToAdvisor_BatchId",
                table: "AllocStudentToAdvisor",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "IX_AllocStudentToAdvisor_DomainId",
                table: "AllocStudentToAdvisor",
                column: "DomainId");

            migrationBuilder.CreateIndex(
                name: "IX_AllocStudentToAdvisor_StudentId",
                table: "AllocStudentToAdvisor",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchToAdvisors_AdvisorId",
                table: "BatchToAdvisors",
                column: "AdvisorId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchToAdvisors_BatchId",
                table: "BatchToAdvisors",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Fees_AdvisorId",
                table: "Fees",
                column: "AdvisorId");

            migrationBuilder.CreateIndex(
                name: "IX_Fees_StudentId",
                table: "Fees",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllocBatchBranchToStudent");

            migrationBuilder.DropTable(
                name: "AllocStudentToAdvisor");

            migrationBuilder.DropTable(
                name: "BatchToAdvisors");

            migrationBuilder.DropTable(
                name: "Fees");
        }
    }
}
