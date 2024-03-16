using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCourierApi.Migrations
{
    /// <inheritdoc />
    public partial class make_deliveryProcess_owned : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Processes");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.CreateTable(
                name: "Deliveries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InquireId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    RequestStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    Process_IsDelivered = table.Column<bool>(type: "INTEGER", nullable: true),
                    Process_CourierName = table.Column<string>(type: "TEXT", nullable: true),
                    Process_PickupTimestamp = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Process_DeliveryTimestamp = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Process_Notes = table.Column<string>(type: "TEXT", nullable: true),
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
                    table.PrimaryKey("PK_Deliveries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deliveries_Inquiries_InquireId",
                        column: x => x.InquireId,
                        principalTable: "Inquiries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_InquireId",
                table: "Deliveries",
                column: "InquireId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deliveries");

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
                    table.ForeignKey(
                        name: "FK_Requests_Inquiries_InquireId",
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
                    RequestId = table.Column<int>(type: "INTEGER", nullable: false),
                    CourierName = table.Column<string>(type: "TEXT", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDelivered = table.Column<bool>(type: "INTEGER", nullable: false),
                    PickupDate = table.Column<DateTime>(type: "TEXT", nullable: true)
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
                name: "IX_Processes_RequestId",
                table: "Processes",
                column: "RequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_InquireId",
                table: "Requests",
                column: "InquireId",
                unique: true);
        }
    }
}
