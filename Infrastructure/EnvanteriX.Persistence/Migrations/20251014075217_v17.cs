using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnvanteriX.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class v17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Floor",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Room",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "PurchaseDate",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "WarrantyEndDate",
                table: "Assets");

            migrationBuilder.AddColumn<bool>(
                name: "IsRented",
                table: "Assets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "RentalStartDate",
                table: "Assets",
                type: "datetime",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRented",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "RentalStartDate",
                table: "Assets");

            migrationBuilder.AddColumn<string>(
                name: "Floor",
                table: "Locations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Room",
                table: "Locations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "Assets",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "PurchaseDate",
                table: "Assets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "WarrantyEndDate",
                table: "Assets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
