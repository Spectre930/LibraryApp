using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class fixRole1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Roles_RolesId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "RolesId",
                table: "Employees",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_RolesId",
                table: "Employees",
                newName: "IX_Employees_RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Roles_RoleId",
                table: "Employees",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Roles_RoleId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "Employees",
                newName: "RolesId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_RoleId",
                table: "Employees",
                newName: "IX_Employees_RolesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Roles_RolesId",
                table: "Employees",
                column: "RolesId",
                principalTable: "Roles",
                principalColumn: "Id");
        }
    }
}
