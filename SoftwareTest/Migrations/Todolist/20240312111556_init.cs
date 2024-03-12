using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftwareTest.Migrations.Todolist
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CPR",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CPRnr = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CPR__3213E83F7E677FE0", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TodolostTB",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userid = table.Column<int>(type: "int", nullable: true),
                    Items = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_CPR_Todo",
                        column: x => x.userid,
                        principalTable: "CPR",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TodolostTB_userid",
                table: "TodolostTB",
                column: "userid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TodolostTB");

            migrationBuilder.DropTable(
                name: "CPR");
        }
    }
}
