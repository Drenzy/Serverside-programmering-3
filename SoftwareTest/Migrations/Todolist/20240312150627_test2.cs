using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftwareTest.Migrations.Todolist
{
    /// <inheritdoc />
    public partial class test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddPrimaryKey(
                name: "PK_TodolostTb_Id",
                table: "TodolostTB",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TodolostTb_Id",
                table: "TodolostTB");
        }
    }
}
