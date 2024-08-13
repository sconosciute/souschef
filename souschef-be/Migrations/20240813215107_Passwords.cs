using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace souschef_be.Migrations
{
    /// <inheritdoc />
    public partial class Passwords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "pw_hash",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "pw_hash",
                table: "users");
        }
    }
}
