using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnvanteriX.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class v8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Models_AssetTypeId",
                table: "Assets");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_ModelId",
                table: "Assets",
                column: "ModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Models_ModelId",
                table: "Assets",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Models_ModelId",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_Assets_ModelId",
                table: "Assets");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Models_AssetTypeId",
                table: "Assets",
                column: "AssetTypeId",
                principalTable: "Models",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
