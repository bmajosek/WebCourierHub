using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCourierApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Country = table.Column<string>(type: "TEXT", nullable: true),
                    Province = table.Column<string>(type: "TEXT", nullable: true),
                    Town = table.Column<string>(type: "TEXT", nullable: true),
                    Street = table.Column<string>(type: "TEXT", nullable: true),
                    BuildingNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    ApartmentNumber = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inquiries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LengthCM = table.Column<float>(type: "REAL", nullable: false),
                    WidthCM = table.Column<float>(type: "REAL", nullable: false),
                    HeightCM = table.Column<float>(type: "REAL", nullable: false),
                    WeightKG = table.Column<float>(type: "REAL", nullable: false),
                    DeliveryDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    SourceAddressId = table.Column<int>(type: "INTEGER", nullable: false),
                    DestinationAddressId = table.Column<int>(type: "INTEGER", nullable: false),
                    HighPriority = table.Column<bool>(type: "INTEGER", nullable: false),
                    WeekendDelivery = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inquiries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inquiries_Addresses_DestinationAddressId",
                        column: x => x.DestinationAddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inquiries_Addresses_SourceAddressId",
                        column: x => x.SourceAddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inquiries_DestinationAddressId",
                table: "Inquiries",
                column: "DestinationAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Inquiries_SourceAddressId",
                table: "Inquiries",
                column: "SourceAddressId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inquiries");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
