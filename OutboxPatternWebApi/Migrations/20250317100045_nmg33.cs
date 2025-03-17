using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OutboxPatternWebApi.Migrations
{
    /// <inheritdoc />
    public partial class nmg33 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FailMessage",
                table: "orderOutBoxes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FailMessage",
                table: "orderOutBoxes");
        }
    }
}
