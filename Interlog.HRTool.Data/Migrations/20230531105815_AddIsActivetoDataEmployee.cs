using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Interlog.HRTool.Data.Migrations
{
    public partial class AddIsActivetoDataEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Employee",
                newName: "Username");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Employee",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Employee");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Employee",
                newName: "UserName");
        }
    }
}
