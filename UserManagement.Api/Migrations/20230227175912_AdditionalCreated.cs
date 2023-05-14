using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagement.Api.Migrations
{
    public partial class AdditionalCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Class_ClassId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "ClassId",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Class_ClassId",
                table: "AspNetUsers",
                column: "ClassId",
                principalTable: "Class",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Class_ClassId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "ClassId",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Class_ClassId",
                table: "AspNetUsers",
                column: "ClassId",
                principalTable: "Class",
                principalColumn: "Id");
        }
    }
}
