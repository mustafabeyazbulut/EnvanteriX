using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnvanteriX.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class v27 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FromDepartmentId",
                table: "AssetMovements",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ToDepartmentId",
                table: "AssetMovements",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssetMovements_FromDepartmentId",
                table: "AssetMovements",
                column: "FromDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetMovements_ToDepartmentId",
                table: "AssetMovements",
                column: "ToDepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetMovements_Departments_FromDepartmentId",
                table: "AssetMovements",
                column: "FromDepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetMovements_Departments_ToDepartmentId",
                table: "AssetMovements",
                column: "ToDepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetMovements_Departments_FromDepartmentId",
                table: "AssetMovements");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetMovements_Departments_ToDepartmentId",
                table: "AssetMovements");

            migrationBuilder.DropIndex(
                name: "IX_AssetMovements_FromDepartmentId",
                table: "AssetMovements");

            migrationBuilder.DropIndex(
                name: "IX_AssetMovements_ToDepartmentId",
                table: "AssetMovements");

            migrationBuilder.DropColumn(
                name: "FromDepartmentId",
                table: "AssetMovements");

            migrationBuilder.DropColumn(
                name: "ToDepartmentId",
                table: "AssetMovements");
        }
    }
}
