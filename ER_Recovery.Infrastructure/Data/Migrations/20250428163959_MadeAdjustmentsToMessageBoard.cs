using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ER_Recovery.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class MadeAdjustmentsToMessageBoard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserHandle",
                table: "MessageBoard",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserHandle",
                table: "MessageBoard");
        }
    }
}
