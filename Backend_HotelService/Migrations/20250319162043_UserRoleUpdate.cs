using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_HotelService.Migrations
{
    /// <inheritdoc />
    public partial class UserRoleUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserRoleId",
                table: "UserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RolePermissionId",
                table: "RolePermissions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserRoleId",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "RolePermissionId",
                table: "RolePermissions");
        }
    }
}
