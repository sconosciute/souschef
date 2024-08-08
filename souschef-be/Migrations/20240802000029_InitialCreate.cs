using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace souschef_be.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MsgId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MsgText = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MsgId);
                });
            
            // migrationBuilder.CreateTable(
            //     name: "Users",
            //     columns: table => new
            //     {
            //         user_id = table.Column<long>(type: "bigint", nullable: false)
            //             .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //         photo = table.Column<byte[]>(type: "bytea", nullable: true),
            //         username = table.Column<string>(type: "text", nullable: false)
            //             .Annotation("Npgsql:CheckConstraint", "CHECK ( LENGTH(TRIM(username)) > 0 )"),
            //         display_name = table.Column<string>(type: "text", nullable: true),
            //         email = table.Column<string>(type: "text", nullable: true),
            //         firstname = table.Column<string>(type: "text", nullable: true),
            //         lastname = table.Column<string>(type: "text", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Users", x => x.user_id);
            //         table.UniqueConstraint("AK_Users_username", x => x.username);
            //         table.UniqueConstraint("AK_Users_display_name", x => x.display_name);
            //         table.UniqueConstraint("AK_Users_email", x => x.email);
            //     });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");
            // migrationBuilder.DropTable(
            //     name: "Users");
        }
    }
}
