using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitApi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    NextReportingDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    ReportingMultiplier = table.Column<int>(type: "INTEGER", nullable: false),
                    ReportingCurrency = table.Column<string>(type: "TEXT", nullable: false),
                    ShareCurrency = table.Column<string>(type: "TEXT", nullable: false),
                    ShareIsin = table.Column<string>(type: "TEXT", nullable: false),
                    ShareSymbol = table.Column<string>(type: "TEXT", nullable: false),
                    DividendCurrency = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dividends",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PeriodStart = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    PeriodEnd = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    PayoutDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    AmountPerShare = table.Column<double>(type: "REAL", nullable: false),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dividends", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dividends_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reportings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PeriodStart = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    PeriodEnd = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Revenue = table.Column<int>(type: "INTEGER", nullable: false),
                    Earnings = table.Column<int>(type: "INTEGER", nullable: false),
                    EarningsPerShare = table.Column<double>(type: "REAL", nullable: false),
                    TotalAssets = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalLiabilities = table.Column<int>(type: "INTEGER", nullable: false),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reportings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reportings_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SharePrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SharePrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SharePrices_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_Name",
                table: "Companies",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dividends_CompanyId",
                table: "Dividends",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Reportings_CompanyId",
                table: "Reportings",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_SharePrices_CompanyId",
                table: "SharePrices",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dividends");

            migrationBuilder.DropTable(
                name: "Reportings");

            migrationBuilder.DropTable(
                name: "SharePrices");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
