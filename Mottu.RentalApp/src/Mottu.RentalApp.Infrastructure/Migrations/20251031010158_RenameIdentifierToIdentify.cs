using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mottu.RentalApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameIdentifierToIdentify : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Identify",
                table: "motorcycles",
                newName: "Identifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Identifier",
                table: "motorcycles",
                newName: "Identify");
        }
    }
}
