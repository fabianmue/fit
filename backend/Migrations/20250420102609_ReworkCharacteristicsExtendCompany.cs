using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitBackend.Migrations
{
    /// <inheritdoc />
    public partial class ReworkCharacteristicsExtendCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoricValues_CompanyHistoricCharacteristics_CompanyHistor~",
                table: "HistoricValues");

            migrationBuilder.DropTable(
                name: "CompanyCharacteristics");

            migrationBuilder.DropTable(
                name: "CompanyHistoricCharacteristics");

            migrationBuilder.DropTable(
                name: "Characteristics");

            migrationBuilder.DropTable(
                name: "HistoricCharacteristics");

            migrationBuilder.DropIndex(
                name: "IX_HistoricValues_CompanyHistoricCharacteristicId",
                table: "HistoricValues");

            migrationBuilder.DropColumn(
                name: "CompanyHistoricCharacteristicId",
                table: "HistoricValues");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyHistoricCurrencyCharacteristicId",
                table: "HistoricValues",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyHistoricNumberCharacteristicId",
                table: "HistoricValues",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LogoUrl",
                table: "Companies",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string[]>(
                name: "Comments",
                table: "Companies",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);

            migrationBuilder.AddColumn<int>(
                name: "FinancialReportingCurrency",
                table: "Companies",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FinancialReportingInterval",
                table: "Companies",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FinancialReportingMultiplier",
                table: "Companies",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string[]>(
                name: "FinancialReportingSourceUrls",
                table: "Companies",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);

            migrationBuilder.AddColumn<int>(
                name: "StockExchange",
                table: "Companies",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StockExchangeCode",
                table: "Companies",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "StockExchangeCurrency",
                table: "Companies",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "HistoricCurrencyCharacteristics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Label = table.Column<string>(type: "text", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricCurrencyCharacteristics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistoricNumberCharacteristics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Label = table.Column<string>(type: "text", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false),
                    Unit = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricNumberCharacteristics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NumberCharacteristics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Label = table.Column<string>(type: "text", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false),
                    Unit = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumberCharacteristics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TextCharacteristics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Label = table.Column<string>(type: "text", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextCharacteristics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyHistoricCurrencyCharacteristics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Currency = table.Column<int>(type: "integer", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    HistoricCurrencyCharacteristicId = table.Column<Guid>(type: "uuid", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "CompanyHistoricNumberCharacteristics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    HistoricNumberCharacteristicId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyHistoricNumberCharacteristics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyHistoricNumberCharacteristics_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyHistoricNumberCharacteristics_HistoricNumberCharacte~",
                        column: x => x.HistoricNumberCharacteristicId,
                        principalTable: "HistoricNumberCharacteristics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyNumberCharacteristics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    NumberCharacteristicId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyNumberCharacteristics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyNumberCharacteristics_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyNumberCharacteristics_NumberCharacteristics_NumberCh~",
                        column: x => x.NumberCharacteristicId,
                        principalTable: "NumberCharacteristics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyTextCharacteristics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    TextCharacteristicId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyTextCharacteristics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyTextCharacteristics_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyTextCharacteristics_TextCharacteristics_TextCharacte~",
                        column: x => x.TextCharacteristicId,
                        principalTable: "TextCharacteristics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoricValues_CompanyHistoricCurrencyCharacteristicId",
                table: "HistoricValues",
                column: "CompanyHistoricCurrencyCharacteristicId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricValues_CompanyHistoricNumberCharacteristicId",
                table: "HistoricValues",
                column: "CompanyHistoricNumberCharacteristicId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyHistoricCurrencyCharacteristics_CompanyId",
                table: "CompanyHistoricCurrencyCharacteristics",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyHistoricCurrencyCharacteristics_HistoricCurrencyChar~",
                table: "CompanyHistoricCurrencyCharacteristics",
                column: "HistoricCurrencyCharacteristicId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyHistoricNumberCharacteristics_CompanyId",
                table: "CompanyHistoricNumberCharacteristics",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyHistoricNumberCharacteristics_HistoricNumberCharacte~",
                table: "CompanyHistoricNumberCharacteristics",
                column: "HistoricNumberCharacteristicId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyNumberCharacteristics_CompanyId",
                table: "CompanyNumberCharacteristics",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyNumberCharacteristics_NumberCharacteristicId",
                table: "CompanyNumberCharacteristics",
                column: "NumberCharacteristicId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyTextCharacteristics_CompanyId",
                table: "CompanyTextCharacteristics",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyTextCharacteristics_TextCharacteristicId",
                table: "CompanyTextCharacteristics",
                column: "TextCharacteristicId");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricValues_CompanyHistoricCurrencyCharacteristics_Compa~",
                table: "HistoricValues",
                column: "CompanyHistoricCurrencyCharacteristicId",
                principalTable: "CompanyHistoricCurrencyCharacteristics",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricValues_CompanyHistoricNumberCharacteristics_Company~",
                table: "HistoricValues",
                column: "CompanyHistoricNumberCharacteristicId",
                principalTable: "CompanyHistoricNumberCharacteristics",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoricValues_CompanyHistoricCurrencyCharacteristics_Compa~",
                table: "HistoricValues");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoricValues_CompanyHistoricNumberCharacteristics_Company~",
                table: "HistoricValues");

            migrationBuilder.DropTable(
                name: "CompanyHistoricCurrencyCharacteristics");

            migrationBuilder.DropTable(
                name: "CompanyHistoricNumberCharacteristics");

            migrationBuilder.DropTable(
                name: "CompanyNumberCharacteristics");

            migrationBuilder.DropTable(
                name: "CompanyTextCharacteristics");

            migrationBuilder.DropTable(
                name: "HistoricCurrencyCharacteristics");

            migrationBuilder.DropTable(
                name: "HistoricNumberCharacteristics");

            migrationBuilder.DropTable(
                name: "NumberCharacteristics");

            migrationBuilder.DropTable(
                name: "TextCharacteristics");

            migrationBuilder.DropIndex(
                name: "IX_HistoricValues_CompanyHistoricCurrencyCharacteristicId",
                table: "HistoricValues");

            migrationBuilder.DropIndex(
                name: "IX_HistoricValues_CompanyHistoricNumberCharacteristicId",
                table: "HistoricValues");

            migrationBuilder.DropColumn(
                name: "CompanyHistoricCurrencyCharacteristicId",
                table: "HistoricValues");

            migrationBuilder.DropColumn(
                name: "CompanyHistoricNumberCharacteristicId",
                table: "HistoricValues");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "FinancialReportingCurrency",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "FinancialReportingInterval",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "FinancialReportingMultiplier",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "FinancialReportingSourceUrls",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "StockExchange",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "StockExchangeCode",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "StockExchangeCurrency",
                table: "Companies");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyHistoricCharacteristicId",
                table: "HistoricValues",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "LogoUrl",
                table: "Companies",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Characteristics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false),
                    Label = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characteristics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistoricCharacteristics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false),
                    Label = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricCharacteristics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyCharacteristics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CharacteristicId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Unit = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyCharacteristics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyCharacteristics_Characteristics_CharacteristicId",
                        column: x => x.CharacteristicId,
                        principalTable: "Characteristics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyCharacteristics_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyHistoricCharacteristics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    HistoricCharacteristicId = table.Column<Guid>(type: "uuid", nullable: false),
                    Unit = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyHistoricCharacteristics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyHistoricCharacteristics_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyHistoricCharacteristics_HistoricCharacteristics_Hist~",
                        column: x => x.HistoricCharacteristicId,
                        principalTable: "HistoricCharacteristics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoricValues_CompanyHistoricCharacteristicId",
                table: "HistoricValues",
                column: "CompanyHistoricCharacteristicId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyCharacteristics_CharacteristicId",
                table: "CompanyCharacteristics",
                column: "CharacteristicId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyCharacteristics_CompanyId",
                table: "CompanyCharacteristics",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyHistoricCharacteristics_CompanyId",
                table: "CompanyHistoricCharacteristics",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyHistoricCharacteristics_HistoricCharacteristicId",
                table: "CompanyHistoricCharacteristics",
                column: "HistoricCharacteristicId");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricValues_CompanyHistoricCharacteristics_CompanyHistor~",
                table: "HistoricValues",
                column: "CompanyHistoricCharacteristicId",
                principalTable: "CompanyHistoricCharacteristics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
