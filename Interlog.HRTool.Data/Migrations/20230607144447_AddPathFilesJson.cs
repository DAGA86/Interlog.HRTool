using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Interlog.HRTool.Data.Migrations
{
    public partial class AddPathFilesJson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Translation",
                columns: new[] { "Key", "LanguageId", "Value" },
                values: new object[] { "datatables.translation.jsonfile", 1, "/dist/js/en-GB.json" });

            migrationBuilder.InsertData(
                table: "Translation",
                columns: new[] { "Key", "LanguageId", "Value" },
                values: new object[] { "datatables.translation.jsonfile", 2, "/dist/js/fr-FR.json" });

            migrationBuilder.InsertData(
                table: "Translation",
                columns: new[] { "Key", "LanguageId", "Value" },
                values: new object[] { "datatables.translation.jsonfile", 3, "/dist/js/pt-PT.json" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Translation",
                keyColumns: new[] { "Key", "LanguageId" },
                keyValues: new object[] { "datatables.translation.jsonfile", 1 });

            migrationBuilder.DeleteData(
                table: "Translation",
                keyColumns: new[] { "Key", "LanguageId" },
                keyValues: new object[] { "datatables.translation.jsonfile", 2 });

            migrationBuilder.DeleteData(
                table: "Translation",
                keyColumns: new[] { "Key", "LanguageId" },
                keyValues: new object[] { "datatables.translation.jsonfile", 3 });
        }
    }
}
