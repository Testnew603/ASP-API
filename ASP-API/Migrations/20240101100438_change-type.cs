using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_API.Migrations
{
    public partial class changetype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advisors_Domains_DomainId",
                table: "Advisors");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviewer_Domains_DomainId",
                table: "Reviewer");

            migrationBuilder.AlterColumn<int>(
                name: "DomainId",
                table: "Reviewer",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Tax",
                table: "CourseFees",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DomainId",
                table: "Advisors",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Advisors_Domains_DomainId",
                table: "Advisors",
                column: "DomainId",
                principalTable: "Domains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviewer_Domains_DomainId",
                table: "Reviewer",
                column: "DomainId",
                principalTable: "Domains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advisors_Domains_DomainId",
                table: "Advisors");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviewer_Domains_DomainId",
                table: "Reviewer");

            migrationBuilder.AlterColumn<int>(
                name: "DomainId",
                table: "Reviewer",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Tax",
                table: "CourseFees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DomainId",
                table: "Advisors",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Advisors_Domains_DomainId",
                table: "Advisors",
                column: "DomainId",
                principalTable: "Domains",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviewer_Domains_DomainId",
                table: "Reviewer",
                column: "DomainId",
                principalTable: "Domains",
                principalColumn: "Id");
        }
    }
}
