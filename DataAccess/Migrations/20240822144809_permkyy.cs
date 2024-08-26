using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class permkyy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "UserRole",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "RoleID",
                table: "UserRole",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "PermissionID",
                table: "PermissionRol",
                newName: "PermissionId");

            migrationBuilder.RenameColumn(
                name: "RolID",
                table: "PermissionRol",
                newName: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                table: "UserRole",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionRol_PermissionId",
                table: "PermissionRol",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionRol_RoleId",
                table: "PermissionRol",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionRol_Permission_PermissionId",
                table: "PermissionRol",
                column: "PermissionId",
                principalTable: "Permission",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionRol_Role_RoleId",
                table: "PermissionRol",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Role_RoleId",
                table: "UserRole",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_User_UserId",
                table: "UserRole",
                column: "UserId",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionRol_Permission_PermissionId",
                table: "PermissionRol");

            migrationBuilder.DropForeignKey(
                name: "FK_PermissionRol_Role_RoleId",
                table: "PermissionRol");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Role_RoleId",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_User_UserId",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_UserRole_UserId",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_PermissionRol_PermissionId",
                table: "PermissionRol");

            migrationBuilder.DropIndex(
                name: "IX_PermissionRol_RoleId",
                table: "PermissionRol");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserRole",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "UserRole",
                newName: "RoleID");

            migrationBuilder.RenameColumn(
                name: "PermissionId",
                table: "PermissionRol",
                newName: "PermissionID");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "PermissionRol",
                newName: "RolID");
        }
    }
}
