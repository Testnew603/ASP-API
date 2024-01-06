using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_API.Migrations
{
    public partial class StudentAgreement_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAgreement_CondonationFees_CondonationFeeId",
                table: "StudentAgreement");

            migrationBuilder.DropIndex(
                name: "IX_StudentAgreement_CondonationFeeId",
                table: "StudentAgreement");

            migrationBuilder.DropColumn(
                name: "CondonationFeeId",
                table: "StudentAgreement");

       
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CondonationFeeId",
                table: "StudentAgreement",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StudentAgreement_CondonationFeeId",
                table: "StudentAgreement",
                column: "CondonationFeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAgreement_CondonationFees_CondonationFeeId",
                table: "StudentAgreement",
                column: "CondonationFeeId",
                principalTable: "CondonationFees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
