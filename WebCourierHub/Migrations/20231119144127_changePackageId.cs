using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCourierHub.Migrations
{
    /// <inheritdoc />
    public partial class changePackageId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inquiry_Package_PackageId",
                table: "Inquiry");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Package",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "PackageId",
                table: "Inquiry",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Inquiry_Package_PackageId",
                table: "Inquiry",
                column: "PackageId",
                principalTable: "Package",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inquiry_Package_PackageId",
                table: "Inquiry");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Package",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<string>(
                name: "PackageId",
                table: "Inquiry",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Inquiry_Package_PackageId",
                table: "Inquiry",
                column: "PackageId",
                principalTable: "Package",
                principalColumn: "Id");
        }
    }
}
