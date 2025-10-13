using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnvanteriX.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class v11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetMovements_Assets_AssetId1",
                table: "AssetMovements");

            migrationBuilder.DropIndex(
                name: "IX_AssetMovements_AssetId1",
                table: "AssetMovements");

            migrationBuilder.DropColumn(
                name: "AssetId1",
                table: "AssetMovements");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssetId1",
                table: "AssetMovements",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssetMovements_AssetId1",
                table: "AssetMovements",
                column: "AssetId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetMovements_Assets_AssetId1",
                table: "AssetMovements",
                column: "AssetId1",
                principalTable: "Assets",
                principalColumn: "Id");
        }
    }
}
