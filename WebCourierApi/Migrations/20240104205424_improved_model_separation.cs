using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCourierApi.Migrations
{
    /// <inheritdoc />
    public partial class improved_model_separation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OfferPOCO",
                table: "OfferPOCO");

            migrationBuilder.DropColumn(
                name: "Pricing_BasePrice",
                table: "OfferPOCO");

            migrationBuilder.DropColumn(
                name: "DeliveryAddress_Country",
                table: "Inquiries");

            migrationBuilder.DropColumn(
                name: "DeliveryAddress_Street",
                table: "Inquiries");

            migrationBuilder.DropColumn(
                name: "Pricing_BasePrice",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "Process_CourierName",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "Process_DeliveryTimestamp",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "Process_IsDelivered",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "Process_Notes",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "Process_PickupTimestamp",
                table: "Deliveries");

            migrationBuilder.RenameColumn(
                name: "Pricing_TaxesTotal",
                table: "OfferPOCO",
                newName: "PricingTaxes");

            migrationBuilder.RenameColumn(
                name: "Pricing_FeesTotal",
                table: "OfferPOCO",
                newName: "PricingFees");

            migrationBuilder.RenameColumn(
                name: "Pricing_Currency",
                table: "OfferPOCO",
                newName: "PricingBase");

            migrationBuilder.RenameColumn(
                name: "PickupAddress_ZipCode",
                table: "Inquiries",
                newName: "PickupZipCode");

            migrationBuilder.RenameColumn(
                name: "PickupAddress_Town",
                table: "Inquiries",
                newName: "PickupTown");

            migrationBuilder.RenameColumn(
                name: "PickupAddress_Street",
                table: "Inquiries",
                newName: "PickupStreet");

            migrationBuilder.RenameColumn(
                name: "PickupAddress_Country",
                table: "Inquiries",
                newName: "DeliveryZipCode");

            migrationBuilder.RenameColumn(
                name: "PickupAddress_BuildingNumber",
                table: "Inquiries",
                newName: "PickupCountryId");

            migrationBuilder.RenameColumn(
                name: "PickupAddress_ApartmentNumber",
                table: "Inquiries",
                newName: "PickupApartmentNumber");

            migrationBuilder.RenameColumn(
                name: "DeliveryAddress_ZipCode",
                table: "Inquiries",
                newName: "DeliveryTown");

            migrationBuilder.RenameColumn(
                name: "DeliveryAddress_Town",
                table: "Inquiries",
                newName: "DeliveryStreet");

            migrationBuilder.RenameColumn(
                name: "DeliveryAddress_BuildingNumber",
                table: "Inquiries",
                newName: "PickupBuildingNumber");

            migrationBuilder.RenameColumn(
                name: "DeliveryAddress_ApartmentNumber",
                table: "Inquiries",
                newName: "DeliveryApartmentNumber");

            migrationBuilder.RenameColumn(
                name: "RequestStatus",
                table: "Deliveries",
                newName: "PricingCurrencyId");

            migrationBuilder.RenameColumn(
                name: "Pricing_TaxesTotal",
                table: "Deliveries",
                newName: "PricingTaxes");

            migrationBuilder.RenameColumn(
                name: "Pricing_FeesTotal",
                table: "Deliveries",
                newName: "PricingFees");

            migrationBuilder.RenameColumn(
                name: "Pricing_Currency",
                table: "Deliveries",
                newName: "PricingBase");

            migrationBuilder.AddColumn<int>(
                name: "PricingCurrencyId",
                table: "OfferPOCO",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeliveryBuildingNumber",
                table: "Inquiries",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeliveryCountryId",
                table: "Inquiries",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsPending",
                table: "Deliveries",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OfferPOCO",
                table: "OfferPOCO",
                columns: new[] { "OfferNumber", "InquireId" });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    ShortName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryProcess",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsDelivered = table.Column<bool>(type: "INTEGER", nullable: false),
                    PickupCourierName = table.Column<string>(type: "TEXT", nullable: true),
                    DeliveryCourierName = table.Column<string>(type: "TEXT", nullable: true),
                    PickupTimestamp = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeliveryTimestamp = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    DeliveryRequestId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryProcess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryProcess_Deliveries_DeliveryRequestId",
                        column: x => x.DeliveryRequestId,
                        principalTable: "Deliveries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CurrencyId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Countries_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OfferPOCO_InquireId",
                table: "OfferPOCO",
                column: "InquireId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferPOCO_PricingCurrencyId",
                table: "OfferPOCO",
                column: "PricingCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Inquiries_DeliveryCountryId",
                table: "Inquiries",
                column: "DeliveryCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Inquiries_PickupCountryId",
                table: "Inquiries",
                column: "PickupCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_PricingCurrencyId",
                table: "Deliveries",
                column: "PricingCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_CurrencyId",
                table: "Countries",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryProcess_DeliveryRequestId",
                table: "DeliveryProcess",
                column: "DeliveryRequestId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Currencies_PricingCurrencyId",
                table: "Deliveries",
                column: "PricingCurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inquiries_Countries_DeliveryCountryId",
                table: "Inquiries",
                column: "DeliveryCountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inquiries_Countries_PickupCountryId",
                table: "Inquiries",
                column: "PickupCountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OfferPOCO_Currencies_PricingCurrencyId",
                table: "OfferPOCO",
                column: "PricingCurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Currencies_PricingCurrencyId",
                table: "Deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_Inquiries_Countries_DeliveryCountryId",
                table: "Inquiries");

            migrationBuilder.DropForeignKey(
                name: "FK_Inquiries_Countries_PickupCountryId",
                table: "Inquiries");

            migrationBuilder.DropForeignKey(
                name: "FK_OfferPOCO_Currencies_PricingCurrencyId",
                table: "OfferPOCO");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "DeliveryProcess");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OfferPOCO",
                table: "OfferPOCO");

            migrationBuilder.DropIndex(
                name: "IX_OfferPOCO_InquireId",
                table: "OfferPOCO");

            migrationBuilder.DropIndex(
                name: "IX_OfferPOCO_PricingCurrencyId",
                table: "OfferPOCO");

            migrationBuilder.DropIndex(
                name: "IX_Inquiries_DeliveryCountryId",
                table: "Inquiries");

            migrationBuilder.DropIndex(
                name: "IX_Inquiries_PickupCountryId",
                table: "Inquiries");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_PricingCurrencyId",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "PricingCurrencyId",
                table: "OfferPOCO");

            migrationBuilder.DropColumn(
                name: "DeliveryBuildingNumber",
                table: "Inquiries");

            migrationBuilder.DropColumn(
                name: "DeliveryCountryId",
                table: "Inquiries");

            migrationBuilder.DropColumn(
                name: "IsPending",
                table: "Deliveries");

            migrationBuilder.RenameColumn(
                name: "PricingTaxes",
                table: "OfferPOCO",
                newName: "Pricing_TaxesTotal");

            migrationBuilder.RenameColumn(
                name: "PricingFees",
                table: "OfferPOCO",
                newName: "Pricing_FeesTotal");

            migrationBuilder.RenameColumn(
                name: "PricingBase",
                table: "OfferPOCO",
                newName: "Pricing_Currency");

            migrationBuilder.RenameColumn(
                name: "PickupZipCode",
                table: "Inquiries",
                newName: "PickupAddress_ZipCode");

            migrationBuilder.RenameColumn(
                name: "PickupTown",
                table: "Inquiries",
                newName: "PickupAddress_Town");

            migrationBuilder.RenameColumn(
                name: "PickupStreet",
                table: "Inquiries",
                newName: "PickupAddress_Street");

            migrationBuilder.RenameColumn(
                name: "PickupCountryId",
                table: "Inquiries",
                newName: "PickupAddress_BuildingNumber");

            migrationBuilder.RenameColumn(
                name: "PickupBuildingNumber",
                table: "Inquiries",
                newName: "DeliveryAddress_BuildingNumber");

            migrationBuilder.RenameColumn(
                name: "PickupApartmentNumber",
                table: "Inquiries",
                newName: "PickupAddress_ApartmentNumber");

            migrationBuilder.RenameColumn(
                name: "DeliveryZipCode",
                table: "Inquiries",
                newName: "PickupAddress_Country");

            migrationBuilder.RenameColumn(
                name: "DeliveryTown",
                table: "Inquiries",
                newName: "DeliveryAddress_ZipCode");

            migrationBuilder.RenameColumn(
                name: "DeliveryStreet",
                table: "Inquiries",
                newName: "DeliveryAddress_Town");

            migrationBuilder.RenameColumn(
                name: "DeliveryApartmentNumber",
                table: "Inquiries",
                newName: "DeliveryAddress_ApartmentNumber");

            migrationBuilder.RenameColumn(
                name: "PricingTaxes",
                table: "Deliveries",
                newName: "Pricing_TaxesTotal");

            migrationBuilder.RenameColumn(
                name: "PricingFees",
                table: "Deliveries",
                newName: "Pricing_FeesTotal");

            migrationBuilder.RenameColumn(
                name: "PricingCurrencyId",
                table: "Deliveries",
                newName: "RequestStatus");

            migrationBuilder.RenameColumn(
                name: "PricingBase",
                table: "Deliveries",
                newName: "Pricing_Currency");

            migrationBuilder.AddColumn<decimal>(
                name: "Pricing_BasePrice",
                table: "OfferPOCO",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddress_Country",
                table: "Inquiries",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddress_Street",
                table: "Inquiries",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Pricing_BasePrice",
                table: "Deliveries",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Process_CourierName",
                table: "Deliveries",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Process_DeliveryTimestamp",
                table: "Deliveries",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Process_IsDelivered",
                table: "Deliveries",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Process_Notes",
                table: "Deliveries",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Process_PickupTimestamp",
                table: "Deliveries",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OfferPOCO",
                table: "OfferPOCO",
                columns: new[] { "InquireId", "OfferNumber" });
        }
    }
}
