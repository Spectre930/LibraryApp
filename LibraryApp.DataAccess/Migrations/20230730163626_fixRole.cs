using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class fixRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Roles_RolesId",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "RolesId",
                table: "Employees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Roles_RolesId",
                table: "Employees",
                column: "RolesId",
                principalTable: "Roles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Roles_RolesId",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "RolesId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Roles_RolesId",
                table: "Employees",
                column: "RolesId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
