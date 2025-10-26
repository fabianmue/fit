using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitApi.Migrations
{
    /// <inheritdoc />
    public partial class ExtendCompanyAndReporting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Reportings",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "NextReportingDate",
                table: "Companies",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "Story",
                table: "Companies",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Reportings");

            migrationBuilder.DropColumn(
                name: "Story",
                table: "Companies");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "NextReportingDate",
                table: "Companies",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
