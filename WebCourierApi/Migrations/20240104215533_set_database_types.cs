using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCourierApi.Migrations
{
    /// <inheritdoc />
    public partial class set_database_types : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryProcess");

            migrationBuilder.DropTable(
                name: "OfferPOCO");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "PickupDate",
                table: "Inquiries",
                type: "DATETIME",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<bool>(
                name: "DeliveryOptions_WeekendDelivery",
                table: "Inquiries",
                type: "BIT",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<bool>(
                name: "DeliveryOptions_IsForCompany",
                table: "Inquiries",
                type: "BIT",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<bool>(
                name: "DeliveryOptions_HighPriority",
                table: "Inquiries",
                type: "BIT",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DeliveryDate",
                table: "Inquiries",
                type: "DATETIME",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Inquiries",
                type: "DATETIME",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "PricingTaxes",
                table: "Deliveries",
                type: "MONEY",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "PricingFees",
                table: "Deliveries",
                type: "MONEY",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "PricingBase",
                table: "Deliveries",
                type: "MONEY",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModificationDate",
                table: "Deliveries",
                type: "DATETIME",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<bool>(
                name: "IsPending",
                table: "Deliveries",
                type: "BIT",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Deliveries",
                type: "DATETIME",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    InquireId = table.Column<int>(type: "INTEGER", nullable: false),
                    OfferNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    PricingBase = table.Column<decimal>(type: "MONEY", nullable: false),
                    PricingTaxes = table.Column<decimal>(type: "MONEY", nullable: false),
                    PricingFees = table.Column<decimal>(type: "MONEY", nullable: false),
                    PricingCurrencyId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => new { x.OfferNumber, x.InquireId });
                    table.ForeignKey(
                        name: "FK_Offers_Currencies_PricingCurrencyId",
                        column: x => x.PricingCurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Offers_Inquiries_InquireId",
                        column: x => x.InquireId,
                        principalTable: "Inquiries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Processes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsDelivered = table.Column<bool>(type: "BIT", nullable: false),
                    PickupCourierName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    DeliveryCourierName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    PickupTimestamp = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    DeliveryTimestamp = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    DeliveryRequestId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Processes_Deliveries_DeliveryRequestId",
                        column: x => x.DeliveryRequestId,
                        principalTable: "Deliveries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Offers_InquireId",
                table: "Offers",
                column: "InquireId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_PricingCurrencyId",
                table: "Offers",
                column: "PricingCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Processes_DeliveryRequestId",
                table: "Processes",
                column: "DeliveryRequestId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "Processes");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "PickupDate",
                table: "Inquiries",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "DATETIME");

            migrationBuilder.AlterColumn<bool>(
                name: "DeliveryOptions_WeekendDelivery",
                table: "Inquiries",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "BIT");

            migrationBuilder.AlterColumn<bool>(
                name: "DeliveryOptions_IsForCompany",
                table: "Inquiries",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "BIT");

            migrationBuilder.AlterColumn<bool>(
                name: "DeliveryOptions_HighPriority",
                table: "Inquiries",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "BIT");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DeliveryDate",
                table: "Inquiries",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "DATETIME");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Inquiries",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME");

            migrationBuilder.AlterColumn<decimal>(
                name: "PricingTaxes",
                table: "Deliveries",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "MONEY");

            migrationBuilder.AlterColumn<decimal>(
                name: "PricingFees",
                table: "Deliveries",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "MONEY");

            migrationBuilder.AlterColumn<decimal>(
                name: "PricingBase",
                table: "Deliveries",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "MONEY");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModificationDate",
                table: "Deliveries",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME");

            migrationBuilder.AlterColumn<bool>(
                name: "IsPending",
                table: "Deliveries",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "BIT");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Deliveries",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME");

            migrationBuilder.CreateTable(
                name: "DeliveryProcess",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DeliveryRequestId = table.Column<int>(type: "INTEGER", nullable: false),
                    DeliveryCourierName = table.Column<string>(type: "TEXT", nullable: true),
                    DeliveryTimestamp = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDelivered = table.Column<bool>(type: "INTEGER", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    PickupCourierName = table.Column<string>(type: "TEXT", nullable: true),
                    PickupTimestamp = table.Column<DateTime>(type: "TEXT", nullable: true)
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
                name: "OfferPOCO",
                columns: table => new
                {
                    OfferNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    InquireId = table.Column<int>(type: "INTEGER", nullable: false),
                    PricingCurrencyId = table.Column<int>(type: "INTEGER", nullable: false),
                    PricingBase = table.Column<decimal>(type: "TEXT", nullable: false),
                    PricingFees = table.Column<decimal>(type: "TEXT", nullable: false),
                    PricingTaxes = table.Column<decimal>(type: "TEXT", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferPOCO", x => new { x.OfferNumber, x.InquireId });
                    table.ForeignKey(
                        name: "FK_OfferPOCO_Currencies_PricingCurrencyId",
                        column: x => x.PricingCurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferPOCO_Inquiries_InquireId",
                        column: x => x.InquireId,
                        principalTable: "Inquiries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryProcess_DeliveryRequestId",
                table: "DeliveryProcess",
                column: "DeliveryRequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OfferPOCO_InquireId",
                table: "OfferPOCO",
                column: "InquireId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferPOCO_PricingCurrencyId",
                table: "OfferPOCO",
                column: "PricingCurrencyId");
        }
    }
}
