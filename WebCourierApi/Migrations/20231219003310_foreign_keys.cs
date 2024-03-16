using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCourierApi.Migrations
{
    /// <inheritdoc />
    public partial class foreign_keys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inquiries_Requests_DeliveryRequestId",
                table: "Inquiries");

            migrationBuilder.DropIndex(
                name: "IX_Inquiries_DeliveryRequestId",
                table: "Inquiries");

            migrationBuilder.DropColumn(
                name: "ProcessId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "DeliveryRequestId",
                table: "Inquiries");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_InquireId",
                table: "Requests",
                column: "InquireId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Inquiries_InquireId",
                table: "Requests",
                column: "InquireId",
                principalTable: "Inquiries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Inquiries_InquireId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_InquireId",
                table: "Requests");

            migrationBuilder.AddColumn<int>(
                name: "ProcessId",
                table: "Requests",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeliveryRequestId",
                table: "Inquiries",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inquiries_DeliveryRequestId",
                table: "Inquiries",
                column: "DeliveryRequestId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Inquiries_Requests_DeliveryRequestId",
                table: "Inquiries",
                column: "DeliveryRequestId",
                principalTable: "Requests",
                principalColumn: "Id");
        }
    }
}
