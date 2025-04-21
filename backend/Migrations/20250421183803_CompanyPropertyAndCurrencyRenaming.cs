using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitBackend.Migrations
{
    /// <inheritdoc />
    public partial class CompanyPropertyAndCurrencyRenaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoricValues_CompanyHistoricCurrencyCharacteristics_Compa~",
                table: "HistoricValues");

            migrationBuilder.DropTable(
                name: "CompanyHistoricCurrencyCharacteristics");

            migrationBuilder.DropTable(
                name: "HistoricCurrencyCharacteristics");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "CompanyHistoricCurrencyCharacteristicId",
                table: "HistoricValues",
                newName: "CompanyHistoricFinancialCharacteristicId");

            migrationBuilder.RenameIndex(
                name: "IX_HistoricValues_CompanyHistoricCurrencyCharacteristicId",
                table: "HistoricValues",
                newName: "IX_HistoricValues_CompanyHistoricFinancialCharacteristicId");

            migrationBuilder.RenameColumn(
                name: "StockExchangeCurrency",
                table: "Companies",
                newName: "StockCurrency");

            migrationBuilder.RenameColumn(
                name: "StockExchangeCode",
                table: "Companies",
                newName: "StockCode");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Companies",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HistoricFinancialCharacteristics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Label = table.Column<string>(type: "text", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricFinancialCharacteristics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyHistoricFinancialCharacteristics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Currency = table.Column<int>(type: "integer", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    HistoricFinancialCharacteristicId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyHistoricFinancialCharacteristics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyHistoricFinancialCharacteristics_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyHistoricFinancialCharacteristics_HistoricFinancialCh~",
                        column: x => x.HistoricFinancialCharacteristicId,
                        principalTable: "HistoricFinancialCharacteristics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyHistoricFinancialCharacteristics_CompanyId",
                table: "CompanyHistoricFinancialCharacteristics",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyHistoricFinancialCharacteristics_HistoricFinancialCh~",
                table: "CompanyHistoricFinancialCharacteristics",
                column: "HistoricFinancialCharacteristicId");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricValues_CompanyHistoricFinancialCharacteristics_Comp~",
                table: "HistoricValues",
                column: "CompanyHistoricFinancialCharacteristicId",
                principalTable: "CompanyHistoricFinancialCharacteristics",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoricValues_CompanyHistoricFinancialCharacteristics_Comp~",
                table: "HistoricValues");

            migrationBuilder.DropTable(
                name: "CompanyHistoricFinancialCharacteristics");

            migrationBuilder.DropTable(
                name: "HistoricFinancialCharacteristics");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "CompanyHistoricFinancialCharacteristicId",
                table: "HistoricValues",
                newName: "CompanyHistoricCurrencyCharacteristicId");

            migrationBuilder.RenameIndex(
                name: "IX_HistoricValues_CompanyHistoricFinancialCharacteristicId",
                table: "HistoricValues",
                newName: "IX_HistoricValues_CompanyHistoricCurrencyCharacteristicId");

            migrationBuilder.RenameColumn(
                name: "StockCurrency",
                table: "Companies",
                newName: "StockExchangeCurrency");

            migrationBuilder.RenameColumn(
                name: "StockCode",
                table: "Companies",
                newName: "StockExchangeCode");

            migrationBuilder.AddColumn<string[]>(
                name: "Comments",
                table: "Companies",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);

            migrationBuilder.CreateTable(
                name: "HistoricCurrencyCharacteristics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false),
                    Label = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricCurrencyCharacteristics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyHistoricCurrencyCharacteristics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    HistoricCurrencyCharacteristicId = table.Column<Guid>(type: "uuid", nullable: false),
                    Currency = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyHistoricCurrencyCharacteristics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyHistoricCurrencyCharacteristics_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyHistoricCurrencyCharacteristics_HistoricCurrencyChar~",
                        column: x => x.HistoricCurrencyCharacteristicId,
                        principalTable: "HistoricCurrencyCharacteristics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyHistoricCurrencyCharacteristics_CompanyId",
                table: "CompanyHistoricCurrencyCharacteristics",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyHistoricCurrencyCharacteristics_HistoricCurrencyChar~",
                table: "CompanyHistoricCurrencyCharacteristics",
                column: "HistoricCurrencyCharacteristicId");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricValues_CompanyHistoricCurrencyCharacteristics_Compa~",
                table: "HistoricValues",
                column: "CompanyHistoricCurrencyCharacteristicId",
                principalTable: "CompanyHistoricCurrencyCharacteristics",
                principalColumn: "Id");
        }
    }
}
