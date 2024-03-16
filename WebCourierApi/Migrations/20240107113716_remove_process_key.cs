using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCourierApi.Migrations
{
    /// <inheritdoc />
    public partial class remove_process_key : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Processes",
                table: "Processes");

            migrationBuilder.DropIndex(
                name: "IX_Processes_DeliveryRequestId",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Processes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Processes",
                table: "Processes",
                column: "DeliveryRequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Processes",
                table: "Processes");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Processes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Processes",
                table: "Processes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Processes_DeliveryRequestId",
                table: "Processes",
                column: "DeliveryRequestId",
                unique: true);
        }
    }
}
