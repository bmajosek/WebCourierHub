using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCourierApi.Migrations
{
    /// <inheritdoc />
    public partial class MadeClientOwned : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Client_ClientDataId",
                table: "Offers");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropIndex(
                name: "IX_Offers_ClientDataId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "ClientDataId",
                table: "Offers");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Offers",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "Client_CompanyName",
                table: "Offers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Client_EmailAddress",
                table: "Offers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Client_FirstName",
                table: "Offers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Client_LastName",
                table: "Offers",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Client_CompanyName",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "Client_EmailAddress",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "Client_FirstName",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "Client_LastName",
                table: "Offers");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Offers",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "ClientDataId",
                table: "Offers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompanyName = table.Column<string>(type: "TEXT", nullable: true),
                    EmailAddress = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Offers_ClientDataId",
                table: "Offers",
                column: "ClientDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Client_ClientDataId",
                table: "Offers",
                column: "ClientDataId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
