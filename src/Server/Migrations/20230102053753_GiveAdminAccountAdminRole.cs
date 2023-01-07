using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RateMyManagementWASM.Server.Migrations
{
    /// <inheritdoc />
    public partial class GiveAdminAccountAdminRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "eb4e7559-826c-48d8-97e0-47926af096d0", "0bbb2a21-f429-49d0-add1-f5b8fc0103f4" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "eb4e7559-826c-48d8-97e0-47926af096d0", "0bbb2a21-f429-49d0-add1-f5b8fc0103f4" });

        }
    }
}
