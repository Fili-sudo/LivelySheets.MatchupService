using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LivelySheets.MatchupService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TableTypoRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OutboxMessages",
                table: "OutboxMessages");

            migrationBuilder.RenameTable(
                name: "OutboxMessages",
                newName: "InboxMessages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InboxMessages",
                table: "InboxMessages",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_InboxMessages",
                table: "InboxMessages");

            migrationBuilder.RenameTable(
                name: "InboxMessages",
                newName: "OutboxMessages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OutboxMessages",
                table: "OutboxMessages",
                column: "Id");
        }
    }
}
