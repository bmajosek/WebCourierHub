using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCourierApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedOffers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inquiries_Addresses_DestinationAddressId",
                table: "Inquiries");

            migrationBuilder.DropForeignKey(
                name: "FK_Inquiries_Addresses_SourceAddressId",
                table: "Inquiries");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Inquiries_DestinationAddressId",
                table: "Inquiries");

            migrationBuilder.DropIndex(
                name: "IX_Inquiries_SourceAddressId",
                table: "Inquiries");

            migrationBuilder.RenameColumn(
                name: "SourceAddressId",
                table: "Inquiries",
                newName: "PickupAddress_BuildingNumber");

            migrationBuilder.RenameColumn(
                name: "DestinationAddressId",
                table: "Inquiries",
                newName: "IsCompany");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Inquiries",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DestinationAddress_ApartmentNumber",
                table: "Inquiries",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DestinationAddress_BuildingNumber",
                table: "Inquiries",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DestinationAddress_Country",
                table: "Inquiries",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DestinationAddress_Province",
                table: "Inquiries",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DestinationAddress_Street",
                table: "Inquiries",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DestinationAddress_Town",
                table: "Inquiries",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PickupAddress_ApartmentNumber",
                table: "Inquiries",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PickupAddress_Country",
                table: "Inquiries",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PickupAddress_Province",
                table: "Inquiries",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PickupAddress_Street",
                table: "Inquiries",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PickupAddress_Town",
                table: "Inquiries",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "PickupDate",
                table: "Inquiries",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmailAddress = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    CompanyName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proposals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InquireId = table.Column<int>(type: "INTEGER", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Pricing_BasePrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    Pricing_TaxesTotal = table.Column<decimal>(type: "TEXT", nullable: false),
                    Pricing_FeesTotal = table.Column<decimal>(type: "TEXT", nullable: false),
                    Pricing_Currency = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proposals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proposals_Inquiries_InquireId",
                        column: x => x.InquireId,
                        principalTable: "Inquiries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InquireId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Pricing_BasePrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    Pricing_TaxesTotal = table.Column<decimal>(type: "TEXT", nullable: false),
                    Pricing_FeesTotal = table.Column<decimal>(type: "TEXT", nullable: false),
                    Pricing_Currency = table.Column<string>(type: "TEXT", nullable: false),
                    ClientDataId = table.Column<int>(type: "INTEGER", nullable: false),
                    OfferStatus = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offers_Client_ClientDataId",
                        column: x => x.ClientDataId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Offers_Inquiries_InquireId",
                        column: x => x.InquireId,
                        principalTable: "Inquiries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Offers_ClientDataId",
                table: "Offers",
                column: "ClientDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_InquireId",
                table: "Offers",
                column: "InquireId");

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_InquireId",
                table: "Proposals",
                column: "InquireId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "Proposals");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Inquiries");

            migrationBuilder.DropColumn(
                name: "DestinationAddress_ApartmentNumber",
                table: "Inquiries");

            migrationBuilder.DropColumn(
                name: "DestinationAddress_BuildingNumber",
                table: "Inquiries");

            migrationBuilder.DropColumn(
                name: "DestinationAddress_Country",
                table: "Inquiries");

            migrationBuilder.DropColumn(
                name: "DestinationAddress_Province",
                table: "Inquiries");

            migrationBuilder.DropColumn(
                name: "DestinationAddress_Street",
                table: "Inquiries");

            migrationBuilder.DropColumn(
                name: "DestinationAddress_Town",
                table: "Inquiries");

            migrationBuilder.DropColumn(
                name: "PickupAddress_ApartmentNumber",
                table: "Inquiries");

            migrationBuilder.DropColumn(
                name: "PickupAddress_Country",
                table: "Inquiries");

            migrationBuilder.DropColumn(
                name: "PickupAddress_Province",
                table: "Inquiries");

            migrationBuilder.DropColumn(
                name: "PickupAddress_Street",
                table: "Inquiries");

            migrationBuilder.DropColumn(
                name: "PickupAddress_Town",
                table: "Inquiries");

            migrationBuilder.DropColumn(
                name: "PickupDate",
                table: "Inquiries");

            migrationBuilder.RenameColumn(
                name: "PickupAddress_BuildingNumber",
                table: "Inquiries",
                newName: "SourceAddressId");

            migrationBuilder.RenameColumn(
                name: "IsCompany",
                table: "Inquiries",
                newName: "DestinationAddressId");

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ApartmentNumber = table.Column<int>(type: "INTEGER", nullable: true),
                    BuildingNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: true),
                    Province = table.Column<string>(type: "TEXT", nullable: true),
                    Street = table.Column<string>(type: "TEXT", nullable: true),
                    Town = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inquiries_DestinationAddressId",
                table: "Inquiries",
                column: "DestinationAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Inquiries_SourceAddressId",
                table: "Inquiries",
                column: "SourceAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inquiries_Addresses_DestinationAddressId",
                table: "Inquiries",
                column: "DestinationAddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inquiries_Addresses_SourceAddressId",
                table: "Inquiries",
                column: "SourceAddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
