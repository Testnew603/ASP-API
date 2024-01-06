using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_API.Migrations
{
    public partial class modelsimplgetset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_StudentAgreement_CondonationFeeId",
                table: "StudentAgreement",
                column: "CondonationFeeId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAgreement_CourseFeeID",
                table: "StudentAgreement",
                column: "CourseFeeID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAgreement_DomainId",
                table: "StudentAgreement",
                column: "DomainId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAgreement_StudentId",
                table: "StudentAgreement",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAgreement_CondonationFees_CondonationFeeId",
                table: "StudentAgreement",
                column: "CondonationFeeId",
                principalTable: "CondonationFees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAgreement_CourseFees_CourseFeeID",
                table: "StudentAgreement",
                column: "CourseFeeID",
                principalTable: "CourseFees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAgreement_Domains_DomainId",
                table: "StudentAgreement",
                column: "DomainId",
                principalTable: "Domains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAgreement_Students_StudentId",
                table: "StudentAgreement",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAgreement_CondonationFees_CondonationFeeId",
                table: "StudentAgreement");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAgreement_CourseFees_CourseFeeID",
                table: "StudentAgreement");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAgreement_Domains_DomainId",
                table: "StudentAgreement");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAgreement_Students_StudentId",
                table: "StudentAgreement");

            migrationBuilder.DropIndex(
                name: "IX_StudentAgreement_CondonationFeeId",
                table: "StudentAgreement");

            migrationBuilder.DropIndex(
                name: "IX_StudentAgreement_CourseFeeID",
                table: "StudentAgreement");

            migrationBuilder.DropIndex(
                name: "IX_StudentAgreement_DomainId",
                table: "StudentAgreement");

            migrationBuilder.DropIndex(
                name: "IX_StudentAgreement_StudentId",
                table: "StudentAgreement");
        }
    }
}
