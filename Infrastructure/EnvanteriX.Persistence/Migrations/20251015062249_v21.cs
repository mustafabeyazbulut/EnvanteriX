using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnvanteriX.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class v21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "MaintenanceRecords");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "MaintenanceRecords",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
