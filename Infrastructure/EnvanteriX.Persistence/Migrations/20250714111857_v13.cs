using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnvanteriX.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class v13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MaintenanceRecords_AssetId",
                table: "MaintenanceRecords");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "MaintenanceRecords",
                newName: "PreServiceDescription");

            migrationBuilder.AddColumn<string>(
                name: "PostServiceDescription",
                table: "MaintenanceRecords",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRecords_AssetId_StartDate",
                table: "MaintenanceRecords",
                columns: new[] { "AssetId", "StartDate" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MaintenanceRecords_AssetId_StartDate",
                table: "MaintenanceRecords");

            migrationBuilder.DropColumn(
                name: "PostServiceDescription",
                table: "MaintenanceRecords");

            migrationBuilder.RenameColumn(
                name: "PreServiceDescription",
                table: "MaintenanceRecords",
                newName: "Description");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRecords_AssetId",
                table: "MaintenanceRecords",
                column: "AssetId");
        }
    }
}
