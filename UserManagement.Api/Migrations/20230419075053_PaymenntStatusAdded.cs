using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagement.Api.Migrations
{
    public partial class PaymenntStatusAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "AspNetUsers");
        }
    }
}
