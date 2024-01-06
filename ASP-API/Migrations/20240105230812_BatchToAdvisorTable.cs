using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_API.Migrations
{
    public partial class BatchToAdvisorTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BatchToAdvisors_Advisors_AdvisorId",
                table: "BatchToAdvisors");

            migrationBuilder.DropForeignKey(
                name: "FK_BatchToAdvisors_Batches_BatchId",
                table: "BatchToAdvisors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BatchToAdvisors",
                table: "BatchToAdvisors");

            migrationBuilder.RenameTable(
                name: "BatchToAdvisors",
                newName: "AllocBatchToAdvisor");

            migrationBuilder.RenameIndex(
                name: "IX_BatchToAdvisors_BatchId",
                table: "AllocBatchToAdvisor",
                newName: "IX_AllocBatchToAdvisor_BatchId");

            migrationBuilder.RenameIndex(
                name: "IX_BatchToAdvisors_AdvisorId",
                table: "AllocBatchToAdvisor",
                newName: "IX_AllocBatchToAdvisor_AdvisorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AllocBatchToAdvisor",
                table: "AllocBatchToAdvisor",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AllocBatchToAdvisor_Advisors_AdvisorId",
                table: "AllocBatchToAdvisor",
                column: "AdvisorId",
                principalTable: "Advisors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AllocBatchToAdvisor_Batches_BatchId",
                table: "AllocBatchToAdvisor",
                column: "BatchId",
                principalTable: "Batches",
                principalColumn: "BatchId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllocBatchToAdvisor_Advisors_AdvisorId",
                table: "AllocBatchToAdvisor");

            migrationBuilder.DropForeignKey(
                name: "FK_AllocBatchToAdvisor_Batches_BatchId",
                table: "AllocBatchToAdvisor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AllocBatchToAdvisor",
                table: "AllocBatchToAdvisor");

            migrationBuilder.RenameTable(
                name: "AllocBatchToAdvisor",
                newName: "BatchToAdvisors");

            migrationBuilder.RenameIndex(
                name: "IX_AllocBatchToAdvisor_BatchId",
                table: "BatchToAdvisors",
                newName: "IX_BatchToAdvisors_BatchId");

            migrationBuilder.RenameIndex(
                name: "IX_AllocBatchToAdvisor_AdvisorId",
                table: "BatchToAdvisors",
                newName: "IX_BatchToAdvisors_AdvisorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BatchToAdvisors",
                table: "BatchToAdvisors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BatchToAdvisors_Advisors_AdvisorId",
                table: "BatchToAdvisors",
                column: "AdvisorId",
                principalTable: "Advisors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BatchToAdvisors_Batches_BatchId",
                table: "BatchToAdvisors",
                column: "BatchId",
                principalTable: "Batches",
                principalColumn: "BatchId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
