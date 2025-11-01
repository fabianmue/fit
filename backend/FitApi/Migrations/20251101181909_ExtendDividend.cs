using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitApi.Migrations
{
    /// <inheritdoc />
    public partial class ExtendDividend : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PayoutDate",
                table: "Dividends",
                newName: "PaymentDate");

            migrationBuilder.AddColumn<DateOnly>(
                name: "ExDividendDate",
                table: "Dividends",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "NextDividendRecordingDate",
                table: "Companies",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExDividendDate",
                table: "Dividends");

            migrationBuilder.DropColumn(
                name: "NextDividendRecordingDate",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "PaymentDate",
                table: "Dividends",
                newName: "PayoutDate");
        }
    }
}
