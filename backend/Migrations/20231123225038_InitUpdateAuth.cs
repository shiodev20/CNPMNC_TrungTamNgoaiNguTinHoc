using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FLCManagementProject.Migrations
{
    /// <inheritdoc />
    public partial class InitUpdateAuth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userRole",
                table: "UserDtos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "userRole",
                table: "UserDtos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
