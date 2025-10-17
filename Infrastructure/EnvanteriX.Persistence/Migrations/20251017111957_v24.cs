using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnvanteriX.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class v24 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastModifiedByEmail",
                table: "Vendors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedByEmail",
                table: "Portal365Settings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedByEmail",
                table: "Models",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedByEmail",
                table: "MaintenanceRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedByEmail",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedByEmail",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedByEmail",
                table: "AssetTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedByEmail",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedByEmail",
                table: "AssetMovements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastModifiedByEmail",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "LastModifiedByEmail",
                table: "Portal365Settings");

            migrationBuilder.DropColumn(
                name: "LastModifiedByEmail",
                table: "Models");

            migrationBuilder.DropColumn(
                name: "LastModifiedByEmail",
                table: "MaintenanceRecords");

            migrationBuilder.DropColumn(
                name: "LastModifiedByEmail",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "LastModifiedByEmail",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "LastModifiedByEmail",
                table: "AssetTypes");

            migrationBuilder.DropColumn(
                name: "LastModifiedByEmail",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "LastModifiedByEmail",
                table: "AssetMovements");
        }
    }
}
