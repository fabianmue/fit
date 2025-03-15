using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitBackend.Migrations
{
    /// <inheritdoc />
    public partial class HistoricCharacteristics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Characteristics");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Characteristics",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "HistoricCharacteristics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Label = table.Column<string>(type: "text", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricCharacteristics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyHistoricCharacteristics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Unit = table.Column<string>(type: "text", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    HistoricCharacteristicId = table.Column<Guid>(type: "uuid", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "HistoricValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CompanyHistoricCharacteristicId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricValues_CompanyHistoricCharacteristics_CompanyHistor~",
                        column: x => x.CompanyHistoricCharacteristicId,
                        principalTable: "CompanyHistoricCharacteristics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyHistoricCharacteristics_CompanyId",
                table: "CompanyHistoricCharacteristics",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyHistoricCharacteristics_HistoricCharacteristicId",
                table: "CompanyHistoricCharacteristics",
                column: "HistoricCharacteristicId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricValues_CompanyHistoricCharacteristicId",
                table: "HistoricValues",
                column: "CompanyHistoricCharacteristicId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoricValues");

            migrationBuilder.DropTable(
                name: "CompanyHistoricCharacteristics");

            migrationBuilder.DropTable(
                name: "HistoricCharacteristics");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Characteristics");

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Characteristics",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
