using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCourierApi.Migrations
{
    /// <inheritdoc />
    public partial class removed_offers_dbset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Inquiries_InquireId",
                table: "Offers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Offers",
                table: "Offers");

            migrationBuilder.RenameTable(
                name: "Offers",
                newName: "OfferPOCO");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Deliveries",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OfferPOCO",
                table: "OfferPOCO",
                columns: new[] { "InquireId", "OfferNumber" });

            migrationBuilder.AddForeignKey(
                name: "FK_OfferPOCO_Inquiries_InquireId",
                table: "OfferPOCO",
                column: "InquireId",
                principalTable: "Inquiries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfferPOCO_Inquiries_InquireId",
                table: "OfferPOCO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OfferPOCO",
                table: "OfferPOCO");

            migrationBuilder.RenameTable(
                name: "OfferPOCO",
                newName: "Offers");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Deliveries",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Offers",
                table: "Offers",
                columns: new[] { "InquireId", "OfferNumber" });

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Inquiries_InquireId",
                table: "Offers",
                column: "InquireId",
                principalTable: "Inquiries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
