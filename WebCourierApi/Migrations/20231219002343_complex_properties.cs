using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCourierApi.Migrations
{
    /// <inheritdoc />
    public partial class complex_properties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Proposals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Offers",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_InquireId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Offers");

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

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "DestinationAddress_Province",
                table: "Inquiries");

            migrationBuilder.RenameColumn(
                name: "OfferStatus",
                table: "Offers",
                newName: "OfferNumber");

            migrationBuilder.RenameColumn(
                name: "WidthCM",
                table: "Inquiries",
                newName: "Package_WidthCM");

            migrationBuilder.RenameColumn(
                name: "WeightKG",
                table: "Inquiries",
                newName: "Package_WeightKG");

            migrationBuilder.RenameColumn(
                name: "WeekendDelivery",
                table: "Inquiries",
                newName: "DeliveryOptions_WeekendDelivery");

            migrationBuilder.RenameColumn(
                name: "LengthCM",
                table: "Inquiries",
                newName: "Package_LengthCM");

            migrationBuilder.RenameColumn(
                name: "HighPriority",
                table: "Inquiries",
                newName: "DeliveryOptions_HighPriority");

            migrationBuilder.RenameColumn(
                name: "HeightCM",
                table: "Inquiries",
                newName: "Package_HeightCM");

            migrationBuilder.RenameColumn(
                name: "DestinationAddress_Town",
                table: "Inquiries",
                newName: "DeliveryAddress_Town");

            migrationBuilder.RenameColumn(
                name: "DestinationAddress_Street",
                table: "Inquiries",
                newName: "DeliveryAddress_Street");

            migrationBuilder.RenameColumn(
                name: "DestinationAddress_Country",
                table: "Inquiries",
                newName: "DeliveryAddress_Country");

            migrationBuilder.RenameColumn(
                name: "DestinationAddress_BuildingNumber",
                table: "Inquiries",
                newName: "DeliveryAddress_BuildingNumber");

            migrationBuilder.RenameColumn(
                name: "DestinationAddress_ApartmentNumber",
                table: "Inquiries",
                newName: "DeliveryAddress_ApartmentNumber");

            migrationBuilder.RenameColumn(
                name: "PickupAddress_Province",
                table: "Inquiries",
                newName: "OwnerKey");

            migrationBuilder.RenameColumn(
                name: "IsCompany",
                table: "Inquiries",
                newName: "DeliveryOptions_IsForCompany");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Inquiries",
                newName: "PickupAddress_ZipCode");

            migrationBuilder.AlterColumn<string>(
                name: "PickupAddress_Town",
                table: "Inquiries",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PickupAddress_Street",
                table: "Inquiries",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PickupAddress_Country",
                table: "Inquiries",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryAddress_Town",
                table: "Inquiries",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryAddress_Street",
                table: "Inquiries",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryAddress_Country",
                table: "Inquiries",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Inquiries",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddress_ZipCode",
                table: "Inquiries",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DeliveryRequestId",
                table: "Inquiries",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Offers",
                table: "Offers",
                columns: new[] { "InquireId", "OfferNumber" });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InquireId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    RequestStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    ProcessId = table.Column<int>(type: "INTEGER", nullable: true),
                    Client_CompanyName = table.Column<string>(type: "TEXT", nullable: true),
                    Client_EmailAddress = table.Column<string>(type: "TEXT", nullable: false),
                    Client_FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    Client_LastName = table.Column<string>(type: "TEXT", nullable: true),
                    Pricing_BasePrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    Pricing_Currency = table.Column<string>(type: "TEXT", nullable: false),
                    Pricing_FeesTotal = table.Column<decimal>(type: "TEXT", nullable: false),
                    Pricing_TaxesTotal = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Processes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RequestId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsDelivered = table.Column<bool>(type: "INTEGER", nullable: false),
                    CourierName = table.Column<string>(type: "TEXT", nullable: true),
                    PickupDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Processes_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inquiries_DeliveryRequestId",
                table: "Inquiries",
                column: "DeliveryRequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Processes_RequestId",
                table: "Processes",
                column: "RequestId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Inquiries_Requests_DeliveryRequestId",
                table: "Inquiries",
                column: "DeliveryRequestId",
                principalTable: "Requests",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inquiries_Requests_DeliveryRequestId",
                table: "Inquiries");

            migrationBuilder.DropTable(
                name: "Processes");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Offers",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Inquiries_DeliveryRequestId",
                table: "Inquiries");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Inquiries");

            migrationBuilder.DropColumn(
                name: "DeliveryAddress_ZipCode",
                table: "Inquiries");

            migrationBuilder.DropColumn(
                name: "DeliveryRequestId",
                table: "Inquiries");

            migrationBuilder.RenameColumn(
                name: "OfferNumber",
                table: "Offers",
                newName: "OfferStatus");

            migrationBuilder.RenameColumn(
                name: "Package_WidthCM",
                table: "Inquiries",
                newName: "WidthCM");

            migrationBuilder.RenameColumn(
                name: "Package_WeightKG",
                table: "Inquiries",
                newName: "WeightKG");

            migrationBuilder.RenameColumn(
                name: "Package_LengthCM",
                table: "Inquiries",
                newName: "LengthCM");

            migrationBuilder.RenameColumn(
                name: "Package_HeightCM",
                table: "Inquiries",
                newName: "HeightCM");

            migrationBuilder.RenameColumn(
                name: "DeliveryOptions_WeekendDelivery",
                table: "Inquiries",
                newName: "WeekendDelivery");

            migrationBuilder.RenameColumn(
                name: "DeliveryOptions_HighPriority",
                table: "Inquiries",
                newName: "HighPriority");

            migrationBuilder.RenameColumn(
                name: "DeliveryAddress_Town",
                table: "Inquiries",
                newName: "DestinationAddress_Town");

            migrationBuilder.RenameColumn(
                name: "DeliveryAddress_Street",
                table: "Inquiries",
                newName: "DestinationAddress_Street");

            migrationBuilder.RenameColumn(
                name: "DeliveryAddress_Country",
                table: "Inquiries",
                newName: "DestinationAddress_Country");

            migrationBuilder.RenameColumn(
                name: "DeliveryAddress_BuildingNumber",
                table: "Inquiries",
                newName: "DestinationAddress_BuildingNumber");

            migrationBuilder.RenameColumn(
                name: "DeliveryAddress_ApartmentNumber",
                table: "Inquiries",
                newName: "DestinationAddress_ApartmentNumber");

            migrationBuilder.RenameColumn(
                name: "PickupAddress_ZipCode",
                table: "Inquiries",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "OwnerKey",
                table: "Inquiries",
                newName: "PickupAddress_Province");

            migrationBuilder.RenameColumn(
                name: "DeliveryOptions_IsForCompany",
                table: "Inquiries",
                newName: "IsCompany");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Offers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

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

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Offers",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "Offers",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "PickupAddress_Town",
                table: "Inquiries",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "PickupAddress_Street",
                table: "Inquiries",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "PickupAddress_Country",
                table: "Inquiries",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "DestinationAddress_Town",
                table: "Inquiries",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "DestinationAddress_Street",
                table: "Inquiries",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "DestinationAddress_Country",
                table: "Inquiries",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "DestinationAddress_Province",
                table: "Inquiries",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Offers",
                table: "Offers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Proposals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InquireId = table.Column<int>(type: "INTEGER", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Pricing_BasePrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    Pricing_Currency = table.Column<string>(type: "TEXT", nullable: false),
                    Pricing_FeesTotal = table.Column<decimal>(type: "TEXT", nullable: false),
                    Pricing_TaxesTotal = table.Column<decimal>(type: "TEXT", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Offers_InquireId",
                table: "Offers",
                column: "InquireId");

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_InquireId",
                table: "Proposals",
                column: "InquireId");
        }
    }
}
