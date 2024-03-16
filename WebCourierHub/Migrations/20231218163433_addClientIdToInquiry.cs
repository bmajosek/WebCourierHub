using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCourierHub.Migrations
{
    /// <inheritdoc />
    public partial class addClientIdToInquiry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "Inquiry",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Inquiry");
        }
    }
}
